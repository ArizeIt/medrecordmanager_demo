using AdvancedMDDomain.DTOs.Responses;
using Microsoft.EntityFrameworkCore;
using PracticeVelocityDomain.DTOs;
using PVAMCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using UrgentCareData;
using UrgentCareData.Models;

namespace PVAMDataService
{
    public class PvDataService : IPvDataService
    {
        #region Patient

        #endregion

        #region Visit

        public bool SavePvXmlRecord(Log_Record pvRecord, string filePath, int sourceProcessId, bool additionalCharge)
        {
            using (var cumcsDb = new UrgentCareContext())
            {
                var pvLogNum = int.Parse(pvRecord.Log_Num);
                var oVisit = cumcsDb.Visit.FirstOrDefault(x => x.PvlogNum == pvLogNum);

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

                    var existingPatient =
                        cumcsDb.PatientInformation.Find(int.Parse(pvRecord.Patient_Information.Pat_NUM));
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
                    var existingPhysican = cumcsDb.Physican.Where(x => x.PvPhysicanId == physicianId);
                    if (existingPhysican.Any())
                    {
                        var matchingPhysican = existingPhysican.FirstOrDefault(x => x.Clinic == pvRecord.Clinic);

                        if (matchingPhysican == null)
                        {
                            oVisit.Physican = new Physican
                            {
                                PvPhysicanId = int.Parse(pvRecord.Physican_ID),
                                FirstName = existingPhysican.First().FirstName,
                                LastName = existingPhysican.First().LastName,
                                DisplayName = pvRecord.Physican,
                                Clinic = pvRecord.Clinic,
                                OfficeKey = cumcsDb.ClinicProfile.FirstOrDefault(x => x.ClinicId == pvRecord.Clinic).OfficeKey.ToString(),
                            };
                        }
                        else
                        {
                            oVisit.Physican = matchingPhysican;
                        }
                    }
                    else
                    {
                        var firstOrDefault = cumcsDb.ClinicProfile.FirstOrDefault(x => x.ClinicId == pvRecord.Clinic);
                        if (firstOrDefault != null)
                            oVisit.Physican = new Physican
                            {
                                PvPhysicanId = int.Parse(pvRecord.Physican_ID),
                                //FirstName = pvRecord.Physican.ParseToList(',')[1],
                                //LastName = pvRecord.Physican.ParseToList(',')[0],
                                DisplayName = pvRecord.Physican,
                                Clinic = pvRecord.Clinic,
                                OfficeKey = firstOrDefault.OfficeKey.ToString(),
                            };
                    }


                    //conditional mapping if record exists

                    if (pvRecord.Patient_Information.GuarantorInformation?.Payer != null)
                    {
                        //add guarantor
                        var guarantor = pvRecord.Patient_Information.GuarantorInformation.Payer;
                        var existingGuarantor = cumcsDb.GuarantorInformation.Find(int.Parse(guarantor.Payer_num));

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
                            var existingIns =
                                cumcsDb.InsuranceInformation.FirstOrDefault(x => x.PrimaryName == payerInfo.Prim_Name);

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
                    cumcsDb.Visit.Add(oVisit);
                }
                else
                {
                    cumcsDb.Visit.Attach(oVisit);
                    cumcsDb.Entry(oVisit).State = EntityState.Modified;
                }
                try
                {
                    var number = cumcsDb.SaveChanges();
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
        }


        public void UpdatePhysician(Physican physicain, Profile amdProfile)
        {
            using (var db = new UrgentCareContext())
            {
                db.Physican.Attach(physicain);
                physicain.AmProviderId = amdProfile.Id;
                physicain.AmdCode = amdProfile.Code;
                physicain.FirstName = amdProfile.Name.Split(',')[1];
                physicain.LastName = amdProfile.Name.Split(',')[0];
                db.SaveChanges();
            }
        }

        public void UpdatePatientLog(int pvPatientId, string amdPatianId, string officekey, int amdImportId)
        {
            using (var db = new UrgentCareContext())
            {

                var existingRec = db.PatientImportLog.FirstOrDefault(x => x.PvpatientId == pvPatientId);
                if (existingRec == null)
                {
                    db.PatientImportLog.Add(new PatientImportLog
                    {
                        AmdimportId = amdImportId,
                        AmdpatientId = amdPatianId,
                        OfficeKey = officekey,
                        PvpatientId = pvPatientId,
                        ImportedDate = DateTime.Now,
                        Status = "AlreadyExists"
                    });
                }

                db.SaveChanges();
            }
        }

        public void UpdateGuarantorLog(int guarantorPayerId, string resPartyId, int amdImportId, string officeKey)
        {
            using (var db = new UrgentCareContext())
            {

                var existingRec = db.GuarantorImportLog.FirstOrDefault(x => x.PayerNumber == guarantorPayerId && x.OfficeKey == officeKey);
                if (existingRec == null)
                {
                    db.GuarantorImportLog.Add(new GuarantorImportLog
                    {
                        AmdimportId = amdImportId,
                        AmdResponsiblePartyId = resPartyId,
                        PayerNumber = guarantorPayerId,
                        ImportedDate = DateTime.Now,
                        OfficeKey = officeKey,
                        Status = "AlreadyExists"
                    });
                }
                db.SaveChanges();
            }
        }


        public void UpdatePatientDoc(string amdDocId, PatientDocument patDoc)
        {
            using (var db = new UrgentCareContext())
            {
                db.PatientDocument.Attach(patDoc);
                patDoc.AmdFileId = amdDocId;
                db.SaveChanges();
            }
        }

        public IList<Visit> GetVisitGraph(int processId)
        {
            using (var db = new UrgentCareContext())
            {
               
                var processIds =
                    db.SourceProcessLog.Where(x => x.ImportedToAmd.Value != true && x.ProcessId == processId)
                        .Select(x => x.ProcessId)
                        .ToList();

                return db.Visit.Include(x => x.PvPatient)
                    .Include(x => x.Chart)
                    .Include(x => x.GuarantorPayer)
                    .Include(x => x.PayerInformation)
                    .Include(x => x.PatientDocument)
                    .Include(x => x.Physican)
                    .Include(x => x.VisitProcCode)
                    .Include(x => x.PayerInformation.Select(y => y.Insurance))
                    .Where(x => processIds.Contains(x.SourceProcessId.Value)).ToList();
            }
        }

        public IList<Visit> GetUnSyncedVisitGraph(DateTime startDate, DateTime endDate)
        {
            using (var db = new UrgentCareContext())
            {
               
                return db.Visit.Include(x => x.PvPatient)
                    .Include(x => x.Chart)
                    .Include(x => x.GuarantorPayer)
                    .Include(x => x.PayerInformation)
                    .Include(x => x.PatientDocument)
                    .Include(x => x.Physican)
                    .Include(x => x.VisitProcCode)
                    .Include(x => x.PayerInformation.Select(y => y.Insurance))
                    .Where(x => !x.VisitImpotLog.Any() && x.ServiceDate >= startDate && x.ServiceDate <= endDate).ToList();
            }
        }

        public IList<ChartDocument> GetChartDocuments(int chartid)
        {
            using (var db = new UrgentCareContext())
            {
                return db.ChartDocument.Where(x => x.ChartId == chartid).ToList();
            }
        }

        public PatientImportLog GetPatientImportByPvId(int patNum, string officeKey)
        {
            using (var db = new UrgentCareContext())
            {
                return db.PatientImportLog.FirstOrDefault(x => x.PvpatientId == patNum && x.OfficeKey == officeKey);
            }
        }

        public PatientImportLog GetPatientImportByAmdId(string amdPatNum)
        {
            using (var db = new UrgentCareContext())
            {
                return db.PatientImportLog.FirstOrDefault(x => x.AmdpatientId == amdPatNum);
            }
        }

        public GuarantorImportLog GetGuarantorImportLog(int payerNumber, string officeKey)
        {
            using (var db = new UrgentCareContext())
            {
                return db.GuarantorImportLog.FirstOrDefault(x => x.PayerNumber == payerNumber && x.OfficeKey == officeKey);
            }
        }


        public InsuranceInformation FindInsuranceInfo(int? insuranceid)
        {
            using (var db = new UrgentCareContext())
            {
                return db.InsuranceInformation.FirstOrDefault(x => x.InsuranceId == insuranceid);
            }
        }
        public VisitImpotLog FindVisitImpotLogByVisitId(int visitId)
        {
            using (var db = new UrgentCareContext())
            {
                return db.VisitImpotLog.FirstOrDefault(x => x.VisitId == visitId);
            }
        }

        public PayerImportLog FindInsurancePayerLog(int payerInfoId)
        {

            using (var db = new UrgentCareContext())
            {
                return db.PayerImportLog.FirstOrDefault(x => x.PayerInfoId == payerInfoId);
            }
        }

        public ChartImportLog FindChartDocLog(int chartDocId)
        {
            using (var db = new UrgentCareContext())
            {
                return db.ChartImportLog.FirstOrDefault(x => x.PvChartDocId == chartDocId);
            }
        }




        public Physican GetAmdProfileId(int pvPhysicanId, int officeKey, string clinic)
        {
            using (var db = new UrgentCareContext())
            {
                var match =
                    db.Physican.FirstOrDefault(
                        x => x.PvPhysicanId == pvPhysicanId && x.Clinic == clinic && x.OfficeKey == officeKey.ToString());
                return match;
            }
        }

        public Physican GetMatchingProfileIdByPv(int pvPhysicanId, int officeKey)
        {
            using (var db = new UrgentCareContext())
            {
                var match =
                    db.Physican.FirstOrDefault(
                        x => x.PvPhysicanId == pvPhysicanId && x.OfficeKey == officeKey.ToString());
                return match;
            }
        }

        public Physican GetDefaultProfile(int officeKey, string clinic)
        {
            using (var db = new UrgentCareContext())
            {
                var match =
                    db.Physican.FirstOrDefault(x => x.OfficeKey == officeKey.ToString() && x.IsDefault);
                return match;
            }
        }

        public IList<Physican> GetAllPhysicians()
        {
            using (var db = new UrgentCareContext())
            {
                return db.Physican.ToList();
            }
        }

        public void SavePatientImportLog(string amdPaitentId, int patnum, string officeKey, int amdLogId)
        {
            using (var db = new UrgentCareContext())
            {
                var newRec = new PatientImportLog
                {
                    AmdimportId = amdLogId,
                    AmdpatientId = amdPaitentId,
                    ImportedDate = DateTime.Now,
                    OfficeKey = officeKey,
                    PvpatientId = patnum,
                    Status = "Added"
                };

                db.PatientImportLog.Add(newRec);
                db.SaveChanges();
            }
        }

        public void SaveResPartyImportLog(int amdImportId, string amdRespartyId, int payerNum, string officeKey)
        {
            using (var db = new UrgentCareContext())
            {
                db.GuarantorImportLog.Add(new GuarantorImportLog
                {
                    ImportedDate = DateTime.Now,
                    AmdimportId = amdImportId,
                    AmdResponsiblePartyId = amdRespartyId,
                    PayerNumber = payerNum,
                    OfficeKey = officeKey,
                    Status = "Added"
                });
                db.SaveChanges();
            }
        }

        public void SaveVisitImportLog(int amdImportId, int visitId, string officeKey, string amdVisitId)
        {
            using (var db = new UrgentCareContext())
            {
                db.VisitImpotLog.Add(new VisitImpotLog
                {
                    ImportedDate = DateTime.Now,
                    OfficeKey = officeKey,
                    VisitId = visitId,
                    AmdimportLogId = amdImportId,
                    AmdvisitId = amdVisitId,
                    Status = "Added"
                });
                db.SaveChanges();
            }
        }


        public void UpdateVisitImportLog(int visitImportid, bool imported)
        {
            using (var db = new UrgentCareContext())
            {
                var rec = db.VisitImpotLog.FirstOrDefault(x => x.Id == visitImportid);
                if (rec != null)
                {
                    rec.ChargeImported = imported;
                }
                db.SaveChanges();
            }
        }

        public void SavePayerImportLog(int amdImportId, string amdInsId, int payerInfoId, string officeKey)
        {
            using (var db = new UrgentCareContext())
            {
                db.PayerImportLog.Add(new PayerImportLog()
                {
                    ImportedDate = DateTime.Now,
                    AmdimportId = amdImportId,
                    AmdpayerId = amdInsId,
                    PayerInfoId = payerInfoId,
                    OfficeKey = officeKey,
                    Status = "Synced"
                });
                db.SaveChanges();
            }
        }

        public void SaveChartImportLog(int amdImportLogId, int chartDocId, string amdFileId)
        {
            using (var db = new UrgentCareContext())
            {
                db.ChartImportLog.Add(new ChartImportLog
                {
                    ImportedDate = DateTime.Now,
                    AmdimportId = amdImportLogId,
                    AmdFileId = amdFileId,
                    PvChartDocId = chartDocId,
                    Status = "Uploaded"
                });
                db.SaveChanges();
            }
        }

        #endregion

        #region LookUp

        public IList<Relationship> GetAllRelationships()
        {
            using (var db = new UrgentCareContext())
            {
                return db.Relationship.ToList();
            }
        }

        public IList<FinClass> GetALlFinclasses(string officeKey)
        {
            using (var db = new UrgentCareContext())
            {
                return db.FinClass.Where(x => x.OfficeKey == officeKey || string.IsNullOrEmpty(x.OfficeKey)).ToList();
            }
        }

        #endregion

        public IList<ProgramConfig> GetProgramConfigs()
        {
            using (var db = new UrgentCareContext())
            {
                return db.ProgramConfig.Where(x => x.Enabled == true).ToList();
            }
        }

        #region Log

        public void SaveProcessLog(SourceProcessLog log)
        {
            using (var cumcsDb = new UrgentCareContext())
            {
                cumcsDb.SourceProcessLog.Add(log);
                try
                {
                    cumcsDb.SaveChanges();
                }
                catch (ValidationException validationException)
                {
                    var s = validationException.ToString();
                    throw new Exception(s);
                }

            }
        }

        public AdvanceMdimportLog SaveAmdImportLog(int sourceProcessId)
        {
            using (var cumcsDb = new UrgentCareContext())
            {
                var newLog = new AdvanceMdimportLog
                {
                    SourceProcessId = sourceProcessId,
                };
                cumcsDb.AdvanceMdimportLog.Add(newLog);
                try
                {
                    var validationContext = new ValidationContext(newLog);
                    Validator.ValidateObject(newLog, validationContext);
                    cumcsDb.SaveChanges();
                    return newLog;
                }
                catch (ValidationException validationException)
                {
                    validationException.ToString();
                    throw;
                }
            }
        }



        public int InitiateNewProcessLog(string sourceFile)
        {
            var newLog = new SourceProcessLog()
            {
                MarkAsProcessed = false,
                MarkDelete = false,
                SourceFileName = sourceFile,
            };
            using (var cumcsDb = new UrgentCareContext())
            {
                cumcsDb.SourceProcessLog.Add(newLog);
                try
                {
                    var validationContext = new ValidationContext(newLog);
                    Validator.ValidateObject(newLog, validationContext);
                    cumcsDb.SaveChanges();
                    return newLog.ProcessId;
                }
                catch (ValidationException validationException)
                {
                    validationException.ToString();
                    throw;
                }

            }
        }

        public void UpdateProcessLogById(int logId, int goodRecCount, int totalRecCount)
        {
            using (var cumcsDb = new UrgentCareContext())
            {
                var processLogRec = cumcsDb.SourceProcessLog.Find(logId);
                try
                {
                    processLogRec.MarkAsProcessed = true;
                    processLogRec.MarkDelete = false;
                    processLogRec.ProcessedDate = DateTime.Now;
                    processLogRec.SuccessFlag = goodRecCount == totalRecCount;
                    processLogRec.ProcessResult =
                        $"{goodRecCount} of total {totalRecCount} were processed from the source file.";

                    var validationContext = new ValidationContext(processLogRec);
                    Validator.ValidateObject(processLogRec, validationContext);
                    cumcsDb.SaveChanges();
                }
                catch (ValidationException validationException)
                {
                    validationException.ToString();
                    throw;
                }

            }

        }

        public bool IfFileProcessed(string filename)
        {
            using (var cumcsDb = new UrgentCareContext())
            {
                return cumcsDb.SourceProcessLog.Any(x => x.SourceFileName == filename && x.ProcessedDate != null);
            }
        }



        public void UpdateVisitLog(string amdVisitId, int visitId, string officeKey, int importId)
        {
            using (var cumcsDb = new UrgentCareContext())
            {
                cumcsDb.VisitImpotLog.Add(new VisitImpotLog
                {
                    AmdimportLogId = importId,
                    AmdvisitId = amdVisitId,
                    ImportedDate = DateTime.Now,
                    OfficeKey = officeKey,
                    VisitId = visitId,
                    Status = "Sucess"
                });
                cumcsDb.SaveChanges();
            }
        }

        public IList<AdvancedMdcolumnHeader> GetColumHeaders(int officeKey)
        {
            using (var cumcdDb = new UrgentCareContext())
            {
                return cumcdDb.AdvancedMdcolumnHeader.Where(x => x.OfficeKey == officeKey).ToList();
            }
        }

        public IList<ClinicProfile> GetClinicsByOfficekey(int officeKey)
        {
            using (var cumcdDb = new UrgentCareContext())
            {
                return cumcdDb.ClinicProfile.Where(x => x.OfficeKey == officeKey).ToList();
            }
        }

        public SourceProcessLog GetSourceProcessLog(string fileName)
        {
            using (var cumcdDb = new UrgentCareContext())
            {
                return cumcdDb.SourceProcessLog.FirstOrDefault(x => x.SourceFileName.Contains(fileName));
            }
        }

        public AdvanceMdimportLog GetImportLogByProcess(int processId)
        {
            using (var cumcdDb = new UrgentCareContext())
            {
                return cumcdDb.AdvanceMdimportLog.FirstOrDefault(x => x.SourceProcessId == processId);
            }
        }

        public IList<PatientImportLog> GetPatientImportLogByOfficeKey(string officeKey)
        {
            using (var cumcdDb = new UrgentCareContext())
            {
                return cumcdDb.PatientImportLog.Where(x => x.OfficeKey == officeKey).ToList();
            }
        }

        #endregion
    }
}
