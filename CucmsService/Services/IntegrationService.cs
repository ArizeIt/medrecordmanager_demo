using AdvancedMDDomain.DTOs.Requests;
using AdvancedMDDomain.DTOs.Responses;
using AdvancedMDInterface;
using CucmsCommon.Models;
using CucmsService.Interfaces;
using Microsoft.EntityFrameworkCore;
using PracticeVelocityDomain.DTOs;
using PVAMCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrgentCareData;
using UrgentCareData.Models;

namespace CucmsService.Services
{
    public class IntegrationService : IIntegrationService
    {

        private readonly ISourceService _sourceService;
        private readonly IFileUploadService _fileUploadService;
        private readonly IVisitService _visitservice;
        private readonly ILoginService _loginService;
        private readonly ILookupService _lookupService;
        private readonly IPatientService _patientService;
        private readonly UrgentCareContext _urgentCareContext;
        public IntegrationService(ISourceService sourceService, IFileUploadService fileUploadService, IVisitService visitservice, ILoginService loginService, ILookupService lookupService, IPatientService patientService, UrgentCareContext urgentCareContext)
        {
            _sourceService = sourceService;
            _fileUploadService = fileUploadService;
            _visitservice = visitservice;
            _loginService = loginService;
            _lookupService = lookupService;
            _patientService = patientService;
            _urgentCareContext = urgentCareContext;
        }

        public async Task ProcessSourceFile(string sourceFilename, string userName)
        {
            var processId = 0;

            var configs = await _urgentCareContext.ProgramConfig.Where(x => x.Enabled).ToListAsync();

            foreach (var config in configs)
            {

                var smtpServer = config.Smtpserver;
                var smtpPort = config.Smtpport;
                var smtpUserName = config.Smtpusername;
                var smtpPassword = config.Smtppassword;
                var fromAddress = config.FromEmailAddress;
                var techAddress = config.TechEmailAddress;
                var toAddress = config.ToEmailAddress.Split(';').ToList();
                var ccAddress = config.CcemailAddress.Split(';').ToList();
                //   var mailer = new Email(smtpServer, int.Parse(smtpPort), smtpUserName, smtpPassword);
                try
                {
                    if (config.SyncOnly.Value)
                    {
                        Console.WriteLine(@"Input the ProcessLogId");
                        var inputProcessId = Console.ReadLine();
                        if (!string.IsNullOrEmpty(inputProcessId))
                        {
                            int.TryParse(inputProcessId, out processId);
                        }
                    }

                    if (config.ProcessSource && !config.SyncOnly.Value)
                    {
                        var pvfilename = config.Pvfilename;
                        var file = string.Empty;
                        if (config.RunAs == "Auto")
                        {
                            if (!string.IsNullOrEmpty(pvfilename))
                            {
                                file = string.Format(pvfilename + "{0}_{1:MM}_{1:dd}_{1:yy}.xml", config.PvfilePrefix,
                                DateTime.Now.AddDays(-config.OffSetDays));
                            }
                        }
                        else if (config.RunAs == "Manual")
                        {

                            file = sourceFilename;

                            while (!File.Exists(config.FilePath + file))
                            {
                                Console.WriteLine(@"Invalid file name, Please provide a valid file. ");
                                file = Console.ReadLine();
                            }
                        }
                        else if (config.RunAs == "New")
                        {
                            file = string.Empty;
                        }

                        var sourceFiles = await _sourceService.GetSourceFilesAsync(config.FilePath, file);
                        if (sourceFiles.Any())
                        {
                            foreach (var sourceFile in sourceFiles)
                            {
                                var processLog = await _urgentCareContext.SourceProcessLog.FirstOrDefaultAsync(x => x.SourceFileName == sourceFile && x.ProcessedDate != null);
                                if (processLog != null && !config.Force)
                                {
                                    processId = processLog.ProcessId;
                                    continue;
                                }

                                var records = await _sourceService.GetPatientRecordsAsync(sourceFile);
                                var goodRecord = 0;
                                var emailbody = string.Empty;

                                //start a new log record imediately

                                var newLog = new SourceProcessLog
                                {
                                    MarkAsProcessed = false,
                                    MarkDelete = false,
                                    SourceFileName = sourceFile,
                                };
                                _urgentCareContext.SourceProcessLog.Add(newLog);
                                await _urgentCareContext.SaveChangesAsync();

                                processId = newLog.ProcessId;

                                if (records.Any())
                                {
                                    foreach (var record in records)
                                    {
                                        //save visit and patient info
                                        bool processFlag;
                                        try
                                        {
                                            processFlag = await SavePvXmlRecord(record, processId, config.AmdofficeKey, config.FilePath, config.AdditionalCharge);
                                        }
                                        catch (Exception ex)
                                        {
                                            processFlag = false;
                                            emailbody += "Failed to import File " + sourceFile + " PvLogNumber: " +
                                                         record.Log_Num +
                                                         Environment.NewLine + "Exception: " + ex.ToString() + Environment.NewLine;
                                        }
                                        if (processFlag)
                                        {
                                            goodRecord += 1;
                                        }

                                    }

                                }
                                //first update the records

                                try
                                {
                                    newLog.MarkAsProcessed = true;
                                    newLog.MarkDelete = false;
                                    newLog.ProcessedDate = DateTime.Now;
                                    newLog.SuccessFlag = goodRecord == records.Count;
                                    newLog.ProcessResult =
                                        $"{goodRecord} of total {records.Count} were processed from the source file.";
                                    _urgentCareContext.SourceProcessLog.Attach(newLog);
                                    await _urgentCareContext.SaveChangesAsync();
                                }
                                catch (ValidationException validationException)
                                {
                                    validationException.ToString();
                                    throw;
                                }


                                if (goodRecord != records.Count)
                                {
                                    // mailer.SendExceptionEmail(fromAddress, techAddress, emailbody);
                                }
                                else
                                {
                                    emailbody = sourceFile + " Import Prcocess is complete on " +
                                                DateTime.Now.ToShortDateString() + "." + Environment.NewLine +
                                                goodRecord + " of " + records.Count + " had been saved successfully. ";
                                    // mailer.SendMail(fromAddress, toAddress, ccAddress, "PV Import Process ", emailbody, string.Empty);
                                }

                            }
                        }
                        else
                        {
                            //TODO: send email : 
                            var subject = $"Important: Integration problem {DateTime.Now.ToString(CultureInfo.InvariantCulture)}";
                            var sb = new StringBuilder();

                            sb.AppendLine("There is no such file exist in the FTP server: " + file + ".");
                            sb.AppendLine("System will try to process the file(s) again next time.");
                            sb.AppendLine(
                                "If you recieve this email again, please contact the source file vendor to confirm the file has been delivery.");

                            //mailer.SendMail(config.FromEmailAddress, new List<string> { config.TechEmailAddress }, null, subject, sb.ToString(), null);

                        }
                    }
                    if (!config.ProcessSource && processId == 0)
                    {

                        var file = string.Format("newExport{0}_{1:MM}_{1:dd}_{1:yy}.xml", config.PvfilePrefix,
                            DateTime.Now.AddDays(-config.OffSetDays));
                        var sourceFound = await _urgentCareContext.SourceProcessLog.FirstOrDefaultAsync(x => x.SourceFileName.Contains(file));

                        if (sourceFound != null)
                        {
                            processId = sourceFound.ProcessId;
                        }
                    }

                    if (processId > 0 && config.AmdSync)

                    {
                        try
                        {
                            var batchJob = new BatchJob()
                            {
                                CreatedDateTime = DateTime.UtcNow,
                                JobStatus = "Created",
                                CreatedBy = userName,
                                TotalRecords = 0
                            };

                            await BatchImportAsync(batchJob, config.AmdofficeKey, DateTime.Today.AddDays(-1), DateTime.Today);
                        }
                        catch (Exception ex)
                        {
                            var mailbody = ex.Message + Environment.NewLine;
                            // mailer.SendExceptionEmail(fromAddress, techAddress, mailbody);
                        }

                    }
                }
                catch (Exception ex)
                {
                    //mailer.SendExceptionEmail(fromAddress, techAddress, ex.Message.ToString());
                }

            }
        }


        public async Task<IList<ImportResult>> BatchImportAsync(BatchJob job, string officeKey, DateTime startDate, DateTime endDate)
        {
            var errors = new List<string>();
            var importFailed = new List<string>();
            var amdPatientId = string.Empty;
            var importResult = new List<ImportResult>();
            //var imported = pvDataService.GetImportLogByProcess(processId);
            //var importLogId = imported == null ? pvDataService.SaveAmdImportLog(processId).Id : imported.Id;



            var visits = await _urgentCareContext.Visit.ToListAsync();

            if (visits != null && visits.Any())
            {
                var config = await _urgentCareContext.ProgramConfig.FirstOrDefaultAsync(x => x.AmdofficeKey == officeKey);
                var response = await _loginService.ProcessLogin(new Uri(config.Apiuri), 1, config.ApiuserName, config.Apipassword, officeKey.ToString(), config.AmdAppName, null);

                if (response.Results == null)
                {
                    //TODO:  log the or email 
                }
                else
                {
                    var apiUrl = new Uri(response.Results.Api);
                    var token = response.Results.Usercontext.Text;

                    foreach (var visitRec in visits)
                    {
                        var providerId = string.Empty;
                        var facilityId = string.Empty;
                        bool existingPatient = false;

                        var findFacility = await _urgentCareContext.AdvancedMdcolumnHeader.FirstOrDefaultAsync(x => x.Clinic == visitRec.ClinicId);

                        if (findFacility == null)
                        {
                            errors.Add("UnMapped Clinic " + visitRec.ClinicId + " Visit reocrd:" + visitRec.PvlogNum + " is skipped");
                        }
                        else
                        {
                            var physician = await _urgentCareContext.Physician.FirstOrDefaultAsync(x => x.PvPhysicianId == visitRec.PhysicianId && x.OfficeKey == officeKey);
                            if (physician != null)
                            {
                                var provider = await LocateProvider(apiUrl, token, physician);
                            }
                            else
                            {

                                importFailed.Add(string.Format("Pysicain was not found, record is skipped. Visit record number:" + visitRec.PvlogNum));
                            }
                            if (!string.IsNullOrEmpty(physician?.AmProviderId))
                            {
                                var importedPatient = await _urgentCareContext.PatientImportLog.FirstOrDefaultAsync(x => x.PvpatientId == visitRec.PvPatientId && x.OfficeKey == officeKey);


                                try
                                {
                                    if (importedPatient != null && importedPatient.Id != 0)
                                    {
                                        //patient in the system, update the records
                                        existingPatient = true;
                                        await UpdateGuarantor(visitRec.VisitId, officeKey);
                                        await AddVisit(apiUrl, token, visitRec.VisitId, officeKey, providerId, amdPatientId, facilityId, existingPatient);

                                    }
                                    else // no previous import , try to insert as new
                                    {
                                        try
                                        {
                                            var respartyId = string.Empty;
                                            //amdPatientId = AddPatient(patientSrv, visitRec.PvPaitentId, importLogId, config.AMDOfficeKey.ToString(), pvDataService,
                                            //    visitRec, lookUpRelations, lookUpFinClasses, physician.AmProviderId, out existingPatient, out respartyId);
                                            if (!string.IsNullOrEmpty(amdPatientId))
                                            {
                                                //if(!string.IsNullOrEmpty(respartyId))
                                                //{
                                                //    UpdateResparty(visitRec, respartyId, patientSrv, errors);
                                                //}
                                                await AddVisit(apiUrl, token, visitRec.VisitId, officeKey, providerId, amdPatientId, facilityId, existingPatient);
                                            }
                                            else
                                            {
                                                //log and inform the problem.
                                                importFailed.Add(string.Format("Patient was not found in AMD and can not be imported. PV PatientId: " + visitRec.PvPatient.PatNum));
                                            }
                                        }
                                        catch (Exception)
                                        {

                                            importFailed.Add(string.Format("Patient can not be imported due to exceptions. PV PatientId: " + visitRec.PvPatient.PatNum));
                                            throw;
                                        }
                                        //can not find previous imported data then add the patient and the log

                                    }

                                    //save the patient notes
                                    if (!string.IsNullOrEmpty(visitRec.Notes))
                                    {
                                        await _patientService.SavePatientNote(apiUrl, token, amdPatientId, amdPatientId, visitRec.Notes);
                                    }

                                    if (visitRec.PayerInformation != null && visitRec.PayerInformation.Any())
                                    {
                                        foreach (var payer in visitRec.PayerInformation)
                                        {

                                            //    AddPayerandInsurer(pvDataService, lookUpSrv, amdPatientId, payer, config.AMDOfficeKey.ToString(), visitRec, lookUpRelations,
                                            //        importLogId, importFailed, errors);
                                            //}

                                        }



                                        if (visitRec.PatientDocument != null && visitRec.PatientDocument.Any() && !string.IsNullOrEmpty(amdPatientId))
                                        {
                                            //upload patient Doc
                                            foreach (var patDoc in visitRec.PatientDocument)
                                            {
                                                if (string.IsNullOrEmpty(patDoc.AmdFileId))
                                                {
                                                    try
                                                    {
                                                        var uploadReesponse = await AddPatientDocAsync(apiUrl, token, patDoc, amdPatientId);
                                                        _urgentCareContext.PatientDocument.Attach(patDoc);
                                                        patDoc.AmdFileId = uploadReesponse.Id;
                                                        await _urgentCareContext.SaveChangesAsync();
                                                    }
                                                    catch
                                                    {
                                                        importFailed.Add(
                                                            string.Format(
                                                                "Failed to add the patient document, document name: {0} , Patient Name: {1}, Pv Patient Id: {2}",
                                                                patDoc.FileName, (visitRec.PvPatient.FirstName + "" + visitRec.PvPatient.LastName), visitRec.PvPatient.PatNum));
                                                    }
                                                }
                                            }
                                        }

                                        if (visitRec.Chart != null)
                                        {
                                            var chartDocs = _urgentCareContext.ChartDocument.Where(x => x.ChartId == visitRec.Chart.ChartId);
                                            if (chartDocs != null && chartDocs.Any() && !string.IsNullOrEmpty(amdPatientId))
                                            {
                                                foreach (var chartDoc in chartDocs)
                                                {
                                                    if (!_urgentCareContext.ChartImportLog.Any(x => x.PvChartDocId == chartDoc.ChartDocId))
                                                    {
                                                        try
                                                        {
                                                            var addfile = await AddChartDocAsync(apiUrl, token, chartDoc, amdPatientId);
                                                            _urgentCareContext.ChartImportLog.Add(new ChartImportLog
                                                            {
                                                                ImportedDate = DateTime.Now,
                                                                AmdimportId = 0, //change this later into amd importlog id,
                                                                AmdFileId = addfile.Id,
                                                                PvChartDocId = chartDoc.ChartDocId,
                                                                Status = "Uploaded"
                                                            });
                                                            await _urgentCareContext.SaveChangesAsync();
                                                        }
                                                        catch
                                                        {
                                                            importFailed.Add(
                                                                string.Format(
                                                                    "Failed to add the patient chart, Chart name: {0} , Patient Name: {1}, Pv Patient Id: {2}",
                                                                    chartDoc.FileName, (visitRec.PvPatient.FirstName + "" + visitRec.PvPatient.LastName), visitRec.PvPatient.PatNum));
                                                        }
                                                    }
                                                }
                                            }

                                        }
                                    }
                                }

                                catch (Exception e)
                                {
                                    errors.Add(e.StackTrace);
                                }
                            }
                        }
                    }
                    if (errors.Any())
                    {
                        var mailbody = errors.Aggregate(string.Empty,
                            (current, error) => current + (error + Environment.NewLine));
                        //mailer.SendExceptionEmail(config.FromEmailAddress, config.TechEmailAddress, mailbody);
                    }

                    if (importFailed.Any())
                    {
                        var mailbody = importFailed.Aggregate(string.Empty,
                            (current, fail) => current + (fail + Environment.NewLine));
                        // mailer.SendResultEmail(config.FromEmailAddress, config.ToEmailAddress.Split(';').ToList(), config.CCEmailAddress.Split(';').ToList(), mailbody);
                    }
                }
            }

            ///await Task.WhenAll();
            return importResult;
        }


        public Task<ImportResult> ImportVisitAsync(int visitId)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseFile> AddChartDocAsync(Uri apiUrl, string apiToken, ChartDocument chartDoc, string amdPatientId)
        {
            if (chartDoc.DocumentImage.Length > 0)
            {
                var content = Convert.ToBase64String(chartDoc.DocumentImage);

                var filenames = chartDoc.FileName.Split('.');
                var uploadRequest = new PpmUploadFileRequest
                {

                    File = new RequestFile
                    {
                        Description = filenames[0],
                        Dos = chartDoc.LastUpdatedOn.ToShortDateString(),
                        Patientid = amdPatientId,
                        Filecontents = content,
                        Fileext = filenames[1],
                        Name = chartDoc.FileName,
                        Filetype = "I",
                        Savechanges = "true",
                        Zipmode = "0",
                        Grouplist = new Grouplist()
                        {
                            Group = new Group
                            {
                                Code = "OURCHT",
                                Name = "Our Charts",
                                Categorylist = new RequestCategorylist
                                {
                                    Category = new RequestCategory
                                    {
                                        Code = "OCLABS",
                                        Default = "0",
                                        Level = "1",
                                        Filetype = "0",
                                        Filegroupfid = "1",
                                        Name = "Labs",
                                        Id = "1"
                                    }
                                }
                            }
                        }
                    }
                };
                var response = await _fileUploadService.Upload(apiUrl, apiToken, uploadRequest);
                if (response.Results != null)
                {
                    //    _pvdataService.SaveChartImportLog(amdImportLogId, chartDoc.ChartDocId, response.Results.Filelist.File.Id);
                    return response.Results.Filelist.File;
                }
            }
            return null;
        }

        public async Task<ResponseFile> AddPatientDocAsync(Uri apiUrl, string apiToken, PatientDocument patDoc, string amdPatientId)
        {
            if (patDoc.FileImage.Length > 0)
            {
                var filenames = patDoc.FileName.Split('.');
                var fileContent = Convert.ToBase64String(patDoc.FileImage);
                var uploadRequest = new PpmUploadFileRequest
                {
                    File = new RequestFile
                    {
                        Description = filenames[0],
                        Dos = patDoc.LastVerifiedOn.ToShortDateString(),
                        Patientid = amdPatientId,
                        Filecontents = fileContent,
                        Fileext = filenames[1],
                        Name = patDoc.FileName,
                        Filetype = "I",
                        Savechanges = "true",
                        Zipmode = "0",
                        Grouplist = new Grouplist()
                        {
                            Group = new Group
                            {
                                Code = "MISC",
                                Name = "Miscellaneous",
                                Categorylist = new RequestCategorylist
                                {
                                    Category = new RequestCategory
                                    {
                                        Code = "MIOTHE",
                                        Default = "0",
                                        Level = "0",
                                        Filetype = "0",
                                        Filegroupfid = "4",
                                        Name = "Other ID",
                                        Id = "23"
                                    }
                                }
                            }
                        }
                    }
                };
                var response = await _fileUploadService.Upload(apiUrl, apiToken, uploadRequest);
                if (response.Results != null)
                {
                    return response.Results.Filelist.File;
                }
            }
            return null;
        }

        public Task<string> UpdateGuarantor(int visitId, string officeKey)
        {
            throw new NotImplementedException();
        }

        public async Task<string> AddVisit(Uri apiUrl, string token, int visitId, string officeKey, string amdProviderId, string amdPatientId, string facilityId, bool existingPateint)
        {
            var visitRec = await _urgentCareContext.Visit.Include(x => x.VisitImportLog).FirstOrDefaultAsync(x => x.VisitId == visitId);

            var newVisit = new Dictionary<string, string>
            {
                { "133358", "ap_type2549" },
                { "132677", "ap_type2562" },
                { "136989", "ap_type1" }
            };
            var oldVisit = new Dictionary<string, string>
            {
                { "133358" ,"ap_type2552" },
                { "132677", "ap_type2548" },
                { "136989", "ap_type1" }
            };
            if (!visitRec.VisitImportLog.Any())
            {
                var visitType = newVisit.FirstOrDefault(x => x.Key == officeKey).Value.ToString();
                if (visitRec.VisitType == "m" || existingPateint)
                {
                    visitType = oldVisit.FirstOrDefault(x => x.Key == officeKey).Value.ToString();
                }
                var advancedMdColumnHeader = await _urgentCareContext.AdvancedMdcolumnHeader.FirstOrDefaultAsync(x => x.Clinic == visitRec.ClinicId);
                if (advancedMdColumnHeader != null)
                {
                    var colHeader = advancedMdColumnHeader.ColumnHeader;
                    var visitResponse = await _visitservice.AddVisit(apiUrl, token, visitRec.ClinicId, amdPatientId, amdProviderId, colHeader, visitType,
                        visitRec.ServiceDate.ToString("MM/dd/yyyy"), visitRec.TimeIn.ToString("HH:mm"), DateTime.Parse(visitRec.TimeOut.ToShortTimeString()).Subtract(DateTime.Parse(visitRec.TimeIn.ToShortTimeString())).TotalMinutes.ToString());

                    if (!string.IsNullOrEmpty(visitResponse?.Results?.Visit.Id))
                    {
                        var visitImportLog = new VisitImportLog
                        {
                            ImportedDate = DateTime.Now,
                            OfficeKey = officeKey,
                            VisitId = visitId,
                            //AmdimportLogId = importLogId,
                            AmdvisitId = visitResponse?.Results?.Visit.Id,
                            Status = "Added"
                        };

                        _urgentCareContext.VisitImportLog.Add(visitImportLog);

                        var savevisitTask = _urgentCareContext.SaveChangesAsync();

                        if (visitRec.CoPayAmount != 0)
                        {
                            var paymentRequest = new PpmAddPaymentRequest
                            {
                                Patient = new AddPaymentPatient
                                {
                                    Amount = visitRec.CoPayAmount.ToString(),
                                    Patientid = amdPatientId,
                                    Paysource = "1",
                                    Paycode = "pp",
                                    Unappliedpaymentlist = new Unappliedpaymentlist
                                    {
                                        Payment = new Payment
                                        {
                                            Source = "1",
                                            Profile = amdProviderId,
                                            Amount = visitRec.CoPayAmount.ToString()
                                        }
                                    },
                                    Depositdate = visitRec.ServiceDate.Date.AddDays(-1).ToString()
                                }
                            };
                            var addPaymentTask = _visitservice.AddPayment(apiUrl, token, paymentRequest);

                        }

                        //Add currentpayment amount 
                        if (visitRec.CurrentPaymentAmount != 0)
                        {
                            var paymentRequest = new PpmAddPaymentRequest
                            {
                                Patient = new AddPaymentPatient
                                {
                                    Amount = visitRec.CurrentPaymentAmount.ToString(),
                                    Patientid = amdPatientId,
                                    Paysource = "1",
                                    Paycode = "pp",
                                    Unappliedpaymentlist = new Unappliedpaymentlist
                                    {
                                        Payment = new Payment
                                        {
                                            Source = "1",
                                            Profile = amdProviderId,
                                            Amount = visitRec.CurrentPaymentAmount.ToString()
                                        }
                                    },
                                    Depositdate = visitRec.ServiceDate.Date.AddDays(-1).ToString()
                                }
                            };
                            var response = await _visitservice.AddPayment(apiUrl, token, paymentRequest);
                        }

                        await SaveCharges(visitRec, amdPatientId, visitImportLog.AmdvisitId, facilityId, amdProviderId, existingPateint);

                        return visitResponse?.Results?.Visit.Id;
                    }
                }
            }
            else if (visitRec.VisitImportLog.FirstOrDefault().ChargeImported == null || visitRec.VisitImportLog.FirstOrDefault().ChargeImported == false)
            {
                var imported = SaveCharges(visitRec, amdPatientId, visitRec.VisitImportLog.FirstOrDefault().AmdvisitId, facilityId, amdProviderId, existingPateint);
                visitRec.VisitImportLog.FirstOrDefault().ChargeImported = true;
                await _urgentCareContext.SaveChangesAsync();
            }
            return string.Empty;
        }

        public Task<bool> SaveCharges(Visit visitRecord, string amdPatientId, string amdVisitId, string amdFacilityId, string providerId, bool existingPantient)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddCharges(Visit visitRecord, string amdPatientId, string amdVisitId, string amdFacilityId, string batchNumber)
        {
            throw new NotImplementedException();
        }

        private async Task<bool> SavePvXmlRecord(Log_Record pvRecord, int sourceProcessId, string officeKey, string filePath, bool additionalCharge)
        {
            var pvLogNum = int.Parse(pvRecord.Log_Num);

            var oVisit = await _urgentCareContext.Visit.FirstOrDefaultAsync(x => x.PvlogNum == pvLogNum);

            if (oVisit == null)
            {
                //save visit and patient info
                oVisit = new Visit
                {
                    CoPayAmount =
                        !string.IsNullOrEmpty(pvRecord.Copay_Amt) ? decimal.Parse(pvRecord.Copay_Amt) : 0,
                    CopayNote = pvRecord.Copay_Notes,
                    CopayType = pvRecord.Copay_Type,
                    CurrentPaymentAmount =
                        !string.IsNullOrEmpty(pvRecord.Curr_Pay_Amt)
                            ? decimal.Parse(pvRecord.Curr_Pay_Amt)
                            : 0,
                    CurrentPaymentNote = pvRecord.Curr_Pay_Notes,
                    CurrentPaymentType = pvRecord.Curr_Pay_Type,
                    DiagCodes = pvRecord.Diag_Codes,
                    Emcode = pvRecord.EM_code,
                    Icdcodes = pvRecord.Diag_Codes_icd10,
                    LastUpdateTime = DateTime.Parse(pvRecord.Last_Upd_DateTime),
                    LastUpdateUser = pvRecord.Last_Upd_UserID,
                    PvlogNum = pvLogNum,
                    PreviousPaymentAmount =
                        !string.IsNullOrEmpty(pvRecord.Prev_Pay_Amt)
                            ? decimal.Parse(pvRecord.Prev_Pay_Amt)
                            : 0,
                    PreviousPaymentType = pvRecord.Prev_Pay_Type,
                    PreviousPaymentNote = pvRecord.Prev_Pay_Notes,
                    ProcCodes = pvRecord.Patient_Information.Proc_codes,
                    ServiceDate = DateTime.Parse(pvRecord.Svc_date),
                    TimeIn = DateTime.Parse(pvRecord.Time_In),
                    TimeOut = DateTime.Parse(pvRecord.Time_out),
                    Notes = pvRecord.Notes,
                    VisitType = pvRecord.VisitType,
                    SourceProcessId = sourceProcessId
                };

                if (!string.IsNullOrEmpty(pvRecord.Patient_Information.Proc_codes))
                {
                    var procList = pvRecord.Patient_Information.Proc_codes.ParseToList('|');
                    if (procList.Any())
                    {
                        foreach (var proc in procList)
                        {
                            if (!string.IsNullOrEmpty(proc))
                            {
                                var procInfo = proc.Split(new[] { ',' }, 2);
                                oVisit.VisitProcCode.Add(new VisitProcCode
                                {
                                    Visit = oVisit,
                                    Quantity = int.Parse(procInfo[0]),
                                    ProcCode =
                                        procInfo[1].EndsWith(",")
                                            ? procInfo[1].TrimEnd(new char[] { ',' })
                                            : procInfo[1]
                                });
                            }
                        }
                    }
                }

                var existingPatient = await
                    _urgentCareContext.PatientInformation.FindAsync(int.Parse(pvRecord.Patient_Information.Pat_NUM));
                if (existingPatient != null)
                {
                    existingPatient.Address1 = pvRecord.Patient_Information.Address1;
                    existingPatient.Address2 = pvRecord.Patient_Information.Address2;
                    existingPatient.Birthday = pvRecord.Patient_Information.Birthday;
                    existingPatient.City = pvRecord.Patient_Information.City;
                    existingPatient.FirstName = pvRecord.Patient_Information.First_Name;
                    existingPatient.LastName = pvRecord.Patient_Information.Last_Name;
                    existingPatient.MiddleName = pvRecord.Patient_Information.Middle_Name;
                    existingPatient.CellPhone = pvRecord.Patient_Information.Cell_Phone;
                    existingPatient.HomePhone = pvRecord.Patient_Information.Home_Phone;
                    existingPatient.Email = pvRecord.Patient_Information.Email;
                    existingPatient.State = pvRecord.Patient_Information.State;
                    existingPatient.Sex = pvRecord.Patient_Information.Sex;
                    existingPatient.Ssn = pvRecord.Patient_Information.SSN;
                    existingPatient.Zip = pvRecord.Patient_Information.Zip;
                    oVisit.PvPatient = existingPatient;
                }
                else
                {
                    oVisit.PvPatient = new PatientInformation
                    {
                        //forgienKey
                        PatNum = int.Parse(pvRecord.Patient_Information.Pat_NUM),
                        Address1 = pvRecord.Patient_Information.Address1,
                        Address2 = pvRecord.Patient_Information.Address2,
                        Birthday = pvRecord.Patient_Information.Birthday,
                        City = pvRecord.Patient_Information.City,
                        FirstName = pvRecord.Patient_Information.First_Name,
                        LastName = pvRecord.Patient_Information.Last_Name,
                        MiddleName = pvRecord.Patient_Information.Middle_Name,
                        CellPhone = pvRecord.Patient_Information.Cell_Phone,
                        HomePhone = pvRecord.Patient_Information.Home_Phone,
                        Email = pvRecord.Patient_Information.Email,
                        State = pvRecord.Patient_Information.State,
                        Sex = pvRecord.Patient_Information.Sex,
                        Ssn = pvRecord.Patient_Information.SSN,
                        Zip = pvRecord.Patient_Information.Zip,
                    };
                }

                var physicianId = int.Parse(pvRecord.Physican_ID);
                var matchingPhysican = await _urgentCareContext.Physician.FirstOrDefaultAsync(x => x.PvPhysicianId == physicianId && x.OfficeKey == officeKey);

                if (matchingPhysican == null)
                {
                    oVisit.Physician = new Physician
                    {
                        PvPhysicianId = int.Parse(pvRecord.Physican_ID),
                        FirstName = pvRecord.Physican.ParseToList(',')[1],
                        LastName = pvRecord.Physican.ParseToList(',')[0],
                        DisplayName = pvRecord.Physican,
                        Clinic = pvRecord.Clinic,
                        OfficeKey = officeKey,
                    };
                }
                else
                {
                    oVisit.Physician = matchingPhysican;
                }


                //conditional mapping if record exists

                if (pvRecord.Patient_Information.GuarantorInformation?.Payer != null)
                {
                    //add guarantor
                    var guarantor = pvRecord.Patient_Information.GuarantorInformation.Payer;
                    var existingGuarantor = await _urgentCareContext.GuarantorInformation.FindAsync(int.Parse(guarantor.Payer_num));

                    if (existingGuarantor != null)
                    {
                        existingGuarantor.Address1 = guarantor.Address1;
                        existingGuarantor.Address2 = guarantor.Address2;
                        existingGuarantor.City = guarantor.City;
                        existingGuarantor.State = guarantor.State;
                        existingGuarantor.Zip = guarantor.Zip;
                        existingGuarantor.FirstName = guarantor.Name.ParseToList(',')[1].Trim();
                        existingGuarantor.LastName = guarantor.Name.ParseToList(',')[0].Trim();
                        existingGuarantor.Phone = guarantor.Phone;
                        existingGuarantor.RelationshipCode = guarantor.Relationship;
                        oVisit.GuarantorPayer = existingGuarantor;
                    }
                    else
                    {
                        oVisit.GuarantorPayer = new UrgentCareData.Models.GuarantorInformation
                        {
                            PayerNum = int.Parse(guarantor.Payer_num),
                            Address1 = guarantor.Address1,
                            Address2 = guarantor.Address2,
                            City = guarantor.City,
                            State = guarantor.State,
                            Zip = guarantor.Zip,
                            FirstName = guarantor.Name.ParseToList(',')[1].Trim(),
                            LastName = guarantor.Name.ParseToList(',')[0].Trim(),
                            Phone = guarantor.Phone,
                            RelationshipCode = guarantor.Relationship,
                            PvPatientId = int.Parse(pvRecord.Patient_Information.Pat_NUM)
                        };

                    }
                }


                //if there is insurance record 
                if (pvRecord.Patient_Information.PvPayerInformation != null)
                {

                    foreach (var payerInfo in pvRecord.Patient_Information.PvPayerInformation.Payers)
                    {
                        var existingIns = await _urgentCareContext.InsuranceInformation.FirstOrDefaultAsync(x => x.PrimaryName == payerInfo.Prim_Name);

                        var newPayer = new PayerInformation
                        {
                            PayerNum = payerInfo.Payer_num != null ? int.Parse(payerInfo.Payer_num) : 0,
                            MemberId = payerInfo.Member_ID,
                            GroupId = payerInfo.Group_ID,
                            Class = int.Parse(payerInfo.Class),
                            Priority = int.Parse(payerInfo.Priority),
                            Type = payerInfo.Type,
                            InsName = !string.IsNullOrEmpty(payerInfo.Ins_Name) ? payerInfo.Ins_Name : string.Empty
                        };

                        if (existingIns != null)
                        {
                            newPayer.Insurance = existingIns;
                        }
                        else
                        {
                            newPayer.Insurance = new InsuranceInformation()
                            {
                                PrimaryName = payerInfo.Prim_Name,
                                PrimaryAddress1 = payerInfo.Prim_Address1,
                                PrimaryAddress2 = payerInfo.Prim_Address2,
                                PrimaryCity = payerInfo.Prim_City,
                                PrimaryState = payerInfo.Prim_State,
                                PrimaryPhone = payerInfo.Prim_Phone,
                                PrimaryZip = payerInfo.Prim_Zip,
                            };
                        }
                        oVisit.PayerInformation.Add(newPayer);
                    }

                }
                var medPay = oVisit.PayerInformation.Any(payer => payer.Type == "MC" || payer.Type == "MD");

                if (additionalCharge)
                {
                    if (!medPay)
                    {
                        oVisit.ProcCodes = string.Concat(oVisit.ProcCodes, "|1,S9088");
                        oVisit.VisitProcCode.Add(new VisitProcCode
                        {
                            Visit = oVisit,
                            Quantity = 1,
                            ProcCode = "S9088"
                        });
                    }

                    if (oVisit.TimeIn.DayOfWeek == DayOfWeek.Saturday || oVisit.TimeIn.DayOfWeek == DayOfWeek.Sunday || oVisit.TimeIn.TimeOfDay.Hours >= 17 && !medPay)
                    {
                        oVisit.ProcCodes = string.Concat(oVisit.ProcCodes, "|1,99051");
                        oVisit.VisitProcCode.Add(new VisitProcCode
                        {
                            Visit = oVisit,
                            Quantity = 1,
                            ProcCode = "99051"
                        });
                    }
                }

                //save medical records
                if (pvRecord.Patient_Information.MedicalRecord != null)
                {
                    var chartRecord = pvRecord.Patient_Information.MedicalRecord.Chart;
                    oVisit.Chart = new UrgentCareData.Models.Chart
                    {
                        DischargedBy = chartRecord.DischargedBy,
                        DischargedDate = DateTime.Parse(chartRecord.DischargedDate),
                        SignOffSealedDate = DateTime.Parse(chartRecord.SignOffSealedDate),
                        SignedOffSealedBy = chartRecord.SignedOffSealedBy,
                    };

                    if (chartRecord.PvChartDocument != null)
                    {
                        var sourceFile = Path.Combine(filePath, chartRecord.PvChartDocument.FileName);
                        var charDoc = new ChartDocument
                        {
                            PatNum = int.Parse(pvRecord.Patient_Information.Pat_NUM),
                            FileName = chartRecord.PvChartDocument.FileName,
                            FileType = short.Parse(chartRecord.PvChartDocument.FileType),
                            LastUpdatedBy = chartRecord.PvChartDocument.LastUpdatedBy,
                            LastUpdatedOn = DateTime.Parse(chartRecord.PvChartDocument.LastUpdatedOn),
                            NumberOfPages = short.Parse(chartRecord.PvChartDocument.NumberOfPages),
                            DocumentImage = File.Exists(sourceFile) ? File.ReadAllBytes(sourceFile) : new byte[0]
                        };

                        oVisit.Chart.ChartDocument.Add(charDoc);
                    }

                }

                if (pvRecord.Patient_Information.PvPatientDocument != null)
                {
                    foreach (var doc in pvRecord.Patient_Information.PvPatientDocument.PatientDocuments)
                    {
                        if (!string.IsNullOrEmpty(doc.FileName))
                        {
                            var sourceFile = Path.Combine(filePath, doc.FileName);
                            oVisit.PatientDocument.Add(
                                new PatientDocument
                                {
                                    LastVerifedBy =
                                        !string.IsNullOrEmpty(doc.LastVerifiedBy) ? doc.LastVerifiedBy : "",
                                    LastVerifiedOn = DateTime.Parse(doc.LastVerifiedOn),
                                    FileImage =
                                        File.Exists(sourceFile) ? File.ReadAllBytes(sourceFile) : new byte[0],
                                    FileType = short.Parse(doc.FileType),
                                    FileName = doc.FileName,
                                    NumofPages = short.Parse(doc.NumberOfPages),
                                    Visit = oVisit
                                });
                        }
                    }
                }
                _urgentCareContext.Visit.Add(oVisit);
            }
            else
            {
                _urgentCareContext.Visit.Attach(oVisit);
                _urgentCareContext.Entry(oVisit).State = EntityState.Modified;
            }
            try
            {
                var number = await _urgentCareContext.SaveChangesAsync();
                return number > 0;
            }
            catch (ValidationException validation)
            {
                validation.ToString();
                return false;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }

        private async Task<Profile> LocateProvider(Uri apiUrl, string token, Physician physician)
        {
            var amdPhysician = new Profile();
            if (string.IsNullOrEmpty(physician.AmProviderId))
            {
                if (!string.IsNullOrEmpty(physician.AmdCode))
                {
                    var providerRes = await _lookupService.LookupProviderByCode(apiUrl, token, physician.AmdCode);

                    amdPhysician = providerRes.Results.Profilelist.Profile.FirstOrDefault();
                }
                else
                {
                    var providerRes = await _lookupService.LookupProviderByName(apiUrl, token, physician.LastName);

                    if (providerRes.Results.Profilelist.Profile.Count != 0 && amdPhysician.Id == null)
                    {
                        foreach (var profile in providerRes.Results.Profilelist.Profile)
                        {
                            if (profile.Name.Trim().ToUpper() == (physician.LastName + ", " + physician.FirstName).ToUpper() || profile.Name.Trim().ToUpper() == (physician.LastName + "," + physician.FirstName).ToUpper())
                            {
                                amdPhysician = profile;
                                break;
                            }
                        }
                    }
                }

            }
            return amdPhysician;
        }

    }
}
