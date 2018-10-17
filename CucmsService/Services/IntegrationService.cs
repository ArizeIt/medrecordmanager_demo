using CucmsCommon.Models;
using CucmsService.Interfaces;
using PVAMDataService;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UrgentCareData.Models;

namespace CucmsService.Services
{
    public class IntegrationService : IIntegrationService
    {
        private readonly IPvDataService _pvdataService;
        private readonly ISourceService _sourceService;
        public IntegrationService(IPvDataService dataSrv, ISourceService sourceService)
        {
            _pvdataService = dataSrv;
            _sourceService = sourceService;
        }

        public async Task ProcessSourceFile(string sourceFilename, string userName)
        {
            var processId = 0;   
            var configs = _pvdataService.GetProgramConfigs();
                foreach (var config in configs.Where(x => x.Enabled))
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
                                    if (_pvdataService.IfFileProcessed(sourceFile) && !config.Force)
                                    {
                                        processId = _pvdataService.GetSourceProcessLog(sourceFile).ProcessId;
                                        continue;
                                    }

                                    var records = await _sourceService.GetPatientRecordsAsync(sourceFile);
                                    var goodRecord = 0;
                                    var emailbody = string.Empty;

                                    //start a new log record imediately
                                    processId = _pvdataService.InitiateNewProcessLog(sourceFile);

                                    if (records.Any())
                                    {
                                        foreach (var record in records)
                                        {
                                            //save visit and patient info
                                            bool processFlag;
                                            try
                                            {
                                                processFlag = _pvdataService.SavePvXmlRecord(record, config.FilePath, processId, config.AdditionalCharge);
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
                                    _pvdataService.UpdateProcessLogById(processId, goodRecord, records.Count);
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
                            var sourceFound = _pvdataService.GetSourceProcessLog(file);
                            if (sourceFound != null)
                            {
                                processId = sourceFound.ProcessId;
                            }
                        }

                        if (processId > 0 && config.AmdSync)

                        {
                            try
                            {
                            var batchJob = new BatchJob() {
                                 CreatedDateTime = DateTime.UtcNow,
                                 JobStatus = "Created",
                                 CreatedBy = userName,
                                 TotalRecords = 0
                            };
                                
                                await BatchImportAsync(batchJob, config, DateTime.Today.AddDays(-1), DateTime.Today);
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

       
        public Task<IList<ImportResult>> BatchImportAsync(BatchJob job, ProgramConfig config, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<ImportResult> ImportVisitAsync(int visitId)
        {
            throw new NotImplementedException();
        }
    }
}
