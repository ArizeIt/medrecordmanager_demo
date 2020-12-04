using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using ExpressionBuilder.Common;
using ExpressionBuilder.Generics;
using ExpressionBuilder.Helpers;
using iText.IO.Image;
using iText.IO.Source;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using MedRecordManager.Extension;
using MedRecordManager.Models;
using MedRecordManager.Models.DailyRecord;
using MedRecordManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PVAMCommon;
using UrgentCareData;
using UrgentCareData.Models;
using UrgentCareData.Queries;

namespace MedRecordManager.Controllers
{
    [Authorize]
    [ResponseCache(NoStore = true, Duration = 0)]
    public class RecordController : Controller
    {
        private readonly UrgentCareContext _urgentCareContext;
        private readonly IViewRenderService _viewRenderService;
        private readonly IEmailSender _emailSrv;
        public RecordController(UrgentCareContext urgentData, IViewRenderService viewRenderService, IEmailSender mailerSrv)
        {
            _urgentCareContext = urgentData;
            _viewRenderService = viewRenderService;
            _emailSrv = mailerSrv;
        }

        [HttpGet]
        public IActionResult Review()
        {
            var vm = new SearchInputs()
            {
                Type = "Daily",

                OfficeKeys = GetAvaliableOfficeKeys()
            };
            return View("RecordView", vm);
        }

        [HttpGet]
        public IActionResult Imported()
        {
            var vm = new SearchInputs()
            {
                Type = "Imported",

                OfficeKeys = GetAvaliableOfficeKeys()
            };
            return View("RecordView", vm);
        }

        [HttpGet]
        public IActionResult Callback()
        {
            var vm = new SearchInputs()
            {
                Type = "Callback",

                OfficeKeys = GetAvaliableOfficeKeys(),

                Clinics = _urgentCareContext.ClinicProfile.DistinctBy(x => x.ClinicId).Select(y =>
                    new SelectListItem
                    {
                        Selected = false,
                        Text = y.ClinicId,
                        Value = y.ClinicId
                    })
            };

            return View("RecordView", vm);
        }

        [HttpGet]
        public IActionResult LoadDaily(int? page, int? limit, string sortBy, string direction, string office, DateTime startDate, DateTime endDate)
        {
            IQueryable<Visit> query;
            var total = 0;
            if (!string.IsNullOrEmpty(office) && startDate != DateTime.MinValue && endDate != DateTime.MinValue)
            {
                var officekeys = office.Split(',').ToList();
                query = _urgentCareContext.Visit.Include(x => x.VisitImpotLog).Include(x => x.Physican).Where(x => officekeys.Contains(x.Physican.OfficeKey.ToString()) && x.ServiceDate >= startDate && x.ServiceDate <= endDate && !x.VisitImpotLog.Any());
            }
            else
            {
                query = _urgentCareContext.Visit.Take(0);
            }
            var records = query.Select(y => new VisitRecordVm()
            {
                VisitId = y.VisitId,
                PatientId = y.PvPatientId,
                ClinicName = y.ClinicId,
                PhysicianName = y.Physican.DisplayName,
                InsuranceName = y.PayerInformation.FirstOrDefault().Insurance.PrimaryName,
                PhysicanId = y.Physican.PvPhysicanId,
                DiagCode = y.DiagCodes.Replace("|", "<br/>"),
                PvRecordId = y.PvlogNum,
                VisitTime = y.ServiceDate.ToShortDateString(),
                PatientName = y.PvPatient.FirstName + " " + y.PvPatient.LastName,
                OfficeKey = y.Physican.OfficeKey,
                PVFinClass = y.PayerInformation.FirstOrDefault().Class.ToString(),
                IcdCodes = y.Icdcodes.Replace("|", "<br/>"),
                Payment = y.CoPayAmount.GetValueOrDefault(0),
                ProcCodes = y.ProcCodes.Replace(",|", "<br/>").Replace("|", "<br/>"),
                IsFlagged = y.Flagged
            }).ToList();

            total = records.Count();

            if (page.HasValue && limit.HasValue)
            {
                var start = (page.Value - 1) * limit.Value;
                records = records.Skip(start).Take(limit.Value).OrderBy(x=>x.VisitTime).ToList();
            }
            return Json(new { records, total });
        }

        [HttpGet]
        public IActionResult LoadCode(int position)
        {
            var vm = new CodeChartVm();
            if (_urgentCareContext.Visit.Any(x => x.Flagged))
            {
                var records = _urgentCareContext.Visit.Include(x => x.VisitProcCode).Include(x => x.Physican).Include(x => x.Chart).ThenInclude(c => c.ChartDocument).Where(x => x.Flagged);
                var visit = records.Skip(position).Take(1).FirstOrDefault();

                if (visit.Chart.ChartDocument.Any())
                {
                    vm.VisitId = visit.VisitId;
                    vm.PhysicanEmail = visit.Physican.Email;
                    vm.PhysicianName = visit.Physican.DisplayName;
                    vm.Chart = new ChartVm
                    {
                        ChartName = visit.Chart.ChartDocument.FirstOrDefault().FileName,
                        FileBinary = visit.Chart.ChartDocument.FirstOrDefault().DocumentImage,
                        IsFlaged = true,
                        ChartType = System.IO.Path.GetExtension(visit.Chart.ChartDocument.FirstOrDefault().FileName)

                    };


                    if (vm.Chart.ChartType.Contains("tif"))
                    {
                        vm.Chart.FileBinary = Utility.CreatePDF(vm.Chart.FileBinary);
                    }

                    vm.Total = records.Count();
                    vm.Position = position + 1;
                }

                InitiatHistory(visit.VisitId);
                //refresh and get a clone of the icd codes
                var originalRecs = _urgentCareContext.Visit.FirstOrDefault(x => x.VisitId == visit.VisitId).Icdcodes.ParseToList('|').Select(x => new Code
                {
                    CodeName = x
                }).ToList();
                var visitHistoryId = _urgentCareContext.Visit.Include(x => x.VisitHistory).FirstOrDefault(x => x.VisitId == visit.VisitId).VisitHistory.FirstOrDefault(x => !x.Saved).VisitHistoryId;


                if (visitHistoryId != 0)
                {
                    var historyCodes = _urgentCareContext.VisitCodeHistory.Where(x => x.VisitHistoryId == visitHistoryId).ToArray();
                    _urgentCareContext.VisitCodeHistory.RemoveRange(historyCodes);

                    foreach (var orec in originalRecs)
                    {
                        _urgentCareContext.VisitCodeHistory.Add(new VisitCodeHistory
                        {
                            VisitHistoryId = visitHistoryId,
                            CodeType = "IcdCode",
                            Code = orec.CodeName,
                            ModifiedBy = HttpContext.User.Identity.Name,
                            ModifiedTime = DateTime.UtcNow,
                            Action = "Cloned"
                        });
                    }


                    var oriCpt = _urgentCareContext.Visit.Include(x => x.VisitProcCode).FirstOrDefault(x => x.VisitId == visit.VisitId).VisitProcCode.ToList();

                    foreach (var cpt in oriCpt)
                    {
                        Utility.ParseModifierCode(cpt.Modifier, out string modifier1, out string modifier2);
                        if (cpt.ProcCode.Contains(","))
                        {
                            var result = cpt.ProcCode.Split(',');
                            cpt.ProcCode = result[0];
                            modifier1 = result[1];
                        }
                        _urgentCareContext.VisitCodeHistory.Add(new VisitCodeHistory
                        {
                            VisitHistoryId = visitHistoryId,
                            CodeType = "CPTCode",
                            Code = cpt.ProcCode,
                            Modifier = modifier1,
                            Modifier2 = modifier2,
                            Quantity = cpt.Quantity.GetValueOrDefault(1),
                            ModifiedBy = HttpContext.User.Identity.Name,
                            ModifiedTime = DateTime.UtcNow,
                            Action = "Cloned"
                        });
                    }

                    var emcode = _urgentCareContext.Visit.Include(x => x.VisitProcCode).FirstOrDefault(x => x.VisitId == visit.VisitId).Emcode;

                    Utility.ParseCptCode(emcode, out string em, out int emQuantity, out string emModifier1, out string emModifier2);
                    
                    //_urgentCareContext.VisitCodeHistory.Add(new VisitCodeHistory
                    //{
                    //    VisitHistoryId = visitHistoryId,
                    //    CodeType = "EM_CPTCode",
                    //    Code = em,
                    //    Modifier = emModifier1,
                    //    Modifier2 = emModifier2,
                    //    Quantity = emQuantity,
                    //    ModifiedBy = HttpContext.User.Identity.Name,
                    //    ModifiedTime = DateTime.UtcNow,
                    //    Action = "Cloned"
                    //});



                    _urgentCareContext.SaveChanges();
                }
            }
            return View("CodeView", vm);
        }

        [HttpGet]
        public IActionResult Payer(int? page, int? limit, string sortBy, string direction, int visitId)
        {

            IQueryable<PayerInformation> query;

            query = _urgentCareContext.PayerInformation.Where(x => visitId == x.VisitId);

            var records = query.ToList();

            var total = records.Count();

            if (page.HasValue && limit.HasValue)
            {
                var start = (page.Value - 1) * limit.Value;
                records = records.Skip(start).Take(limit.Value).ToList();
            }
            return Json(new { records, total });
        }
        
        [HttpGet]
        public IActionResult LoadCallback(int? page, int? limit, string sortBy, string direction, string office, string clinic, DateTime startDate, DateTime endDate)
        {

            IQueryable<Visit> query;
            if (string.IsNullOrEmpty(office) && startDate != DateTime.MinValue && endDate != DateTime.MinValue)
            {
                query = _urgentCareContext.Visit.Where(x => x.ServiceDate > startDate && x.ServiceDate < endDate);
            }
            else
            {
                query = _urgentCareContext.Visit;
            }
            var records = query.Select(y => new PatientVisitVM()
            {
                PvId = y.PvPatientId.ToString(),
                PvClinic = y.ClinicId,

                VisitDate = y.ServiceDate.ToShortDateString(),
                PatientName = y.PvPatient.FirstName + " " + y.PvPatient.LastName,
                PvPhone = y.PvPatient.CellPhone
            }).ToList();

            var total = records.Count();

            if (page.HasValue && limit.HasValue)
            {
                var start = (page.Value - 1) * limit.Value;
                records = records.Skip(start).Take(limit.Value).OrderBy(x=>x.VisitDate).ToList();
            }


            return Json(new { records, total });
        }

        [HttpGet]
        public IActionResult GetDetails(int visitId)
        {
            var detailRecord = new DetailRecord
            {
                VisitId = visitId.ToString(),
                Relationship = _urgentCareContext.Relationship.Where(x => x.AmrelationshipCode != null).Select(x => new SelectListItem
                {
                    Value = x.Hipaarelationship,
                    Text = x.Description
                }).ToList()
            };

            var visitRec = _urgentCareContext.Visit.Include(x => x.PvPatient).Include(x => x.PayerInformation).Include(x => x.GuarantorPayer).Include(x => x.Chart.ChartDocument).Include(x => x.PatientDocument).SingleOrDefault(x => x.VisitId == visitId);

            if (visitRec != null)
            {
                detailRecord.GuarantorInfo = new Guarantor
                {
                    FirstName = visitRec.GuarantorPayer.FirstName,
                    LastName = visitRec.GuarantorPayer.LastName,
                    PayerNum = visitRec.GuarantorPayerId,
                    RelationshipCode = visitRec.GuarantorPayer.RelationshipCode,
                    Address = new Address
                    {
                        Address1 = visitRec.GuarantorPayer.Address1,
                        Address2 = visitRec.GuarantorPayer.Address2,
                        City = visitRec.GuarantorPayer.City,
                        State = visitRec.GuarantorPayer.State,
                        Zip = visitRec.GuarantorPayer.Zip
                    }
                };

                var insuranceIds = visitRec.PayerInformation.DistinctBy(x => x.InsuranceId).Select(y => y.InsuranceId);

                detailRecord.InsuranceInfo = _urgentCareContext.InsuranceInformation.Where(x => insuranceIds.Contains(x.InsuranceId))
                    .Select(x => new Insurance {
                        InsuranceId = x.InsuranceId,
                        Address = new Address
                        {
                            Address1 = x.PrimaryAddress1,
                            Address2 = x.PrimaryAddress2,
                            City = x.PrimaryCity,
                            State = x.PrimaryState,
                            Zip = x.PrimaryZip
                        },
                        InsuranceName = x.PrimaryName,
                        Phone = x.PrimaryPhone,
                        AmdCode = x.AmdCode
                    });

                detailRecord.ChartId = visitRec.ChartId.Value;


                detailRecord.PaitentInfo = new Patient
                {
                    FirstName = visitRec.PvPatient.FirstName,
                    LastName = visitRec.PvPatient.LastName,
                    MidName = visitRec.PvPatient.MiddleName,
                    PvNumber = visitRec.PvPatientId,
                    SSN = visitRec.PvPatient.Ssn,
                    Address = new Address
                    {
                        Address1 = visitRec.PvPatient.Address1,
                        Address2 = visitRec.PvPatient.Address2,
                        City = visitRec.PvPatient.City,
                        State = visitRec.PvPatient.State,
                        Zip = visitRec.PvPatient.Zip
                    },

                    HomePhone = visitRec.PvPatient.HomePhone,
                    CellPhone = visitRec.PvPatient.CellPhone,
                    Email = visitRec.PvPatient.Email
                };

                detailRecord.ChartInfo = new PatientChart
                {
                    ChartId = visitRec.ChartId.GetValueOrDefault(),
                    SignoffBy = visitRec.Chart.SignedOffSealedBy,
                    DiscahrgedBy = visitRec.Chart.DischargedBy,
                    DischargedDate = visitRec.Chart.DischargedDate,
                    SignoffDate = visitRec.Chart.SignOffSealedDate,
                    ChartDocs = visitRec.Chart.ChartDocument.Select(x => new ChartDoc {

                        FileName = x.FileName,
                        FileType = x.FileType,
                        LastUpdatedby = x.LastUpdatedBy,
                        LastUpdatedOn = x.LastUpdatedOn

                    })
                };

                detailRecord.PatientDoc = visitRec.PatientDocument.Select(x => new FileDocument {
                    FileName = x.FileName,
                    Type = x.FileType,
                    LastVerifiedBy = x.LastVerifedBy,
                    LastVeryfiedOn = x.LastVerifiedOn

                });

                if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    _viewRenderService.RenderToStringAsync("DetailView", detailRecord);
                }
            }

            return PartialView("DetailView", detailRecord);

        }

        [HttpGet]
        public async Task<IActionResult> GetClinics()
        {
            var records = await _urgentCareContext.ClinicProfile.Select(x => new { id = x.ClinicId, text = x.ClinicId }).ToListAsync();
            return Json(records);
        }

        [HttpGet]
        public async Task<IActionResult> GetModifiers()
        {
            var records = await _urgentCareContext.Modifier.Select(x => new { id = x.ModifierCode, text = x.ModifierCode }).ToListAsync();
            records.Insert(0, new { id = "", text = "" });
            return Json(records);
        }


        [HttpGet]
        public async Task<IActionResult> GetPhysicians(string clinicId)
        {
            var physicians = await _urgentCareContext.Physican.Where(x => x.Clinic == clinicId).Select(x => new { id = x.PvPhysicanId, text = x.PvPhysicanId }).ToListAsync();
            return Json(physicians);
        }

        [HttpGet]
        public IActionResult  GetAllPhysicians()
        {
            var physicians = _urgentCareContext.Physican.Where(x=> !string.IsNullOrEmpty(x.DisplayName)).OrderBy(x=>x.DisplayName).Select(x => new { id = x.PvPhysicanId, text = x.DisplayName }).DistinctBy(x=>x.id).ToList();
            return Json(physicians);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateVisitAsync(VisitRecordVm record)
        {
            var visit = _urgentCareContext.Visit.Include(x => x.PvPatient).FirstOrDefault(x => x.VisitId == record.VisitId);

            if (visit != null)
            {
                if (visit.ClinicId != record.ClinicName)
                {
                    var newfficeKey = _urgentCareContext.ClinicProfile.FirstOrDefault(x => x.ClinicId == record.ClinicName).OfficeKey;
                    var physicians = _urgentCareContext.Physican.Where(x => x.PvPhysicanId == visit.PhysicanId && x.OfficeKey == newfficeKey);

                    if (!physicians.Any())
                    {
                        visit.PhysicanId = _urgentCareContext.Physican.FirstOrDefault(x => x.OfficeKey == newfficeKey && x.IsDefault).PvPhysicanId;
                    }

                    visit.ClinicId = record.ClinicName;
                    visit.OfficeKey = newfficeKey;
                    visit.IsModified = true;
                    _urgentCareContext.Visit.Attach(visit);

                    var saved = await _urgentCareContext.SaveChangesAsyncWithAudit(User.Identity.Name);

                    if (saved < 0)
                    {
                        return Json(new { success = false, message = "Can not save this record." });
                    }
                }

                else if (visit.PhysicanId != record.PhysicanId)
                {
                    visit.PhysicanId = record.PhysicanId;
                    visit.IsModified = true;
                    _urgentCareContext.Visit.Attach(visit);
                    var saved = await _urgentCareContext.SaveChangesAsyncWithAudit(User.Identity.Name);

                    if (saved < 0)
                    {
                        return Json(new { success = false, message = "Can not save this record." });
                    }

                }
                else
                {
                    return Json(new { success = false, message = "Value is unchanged" });
                }
                return Json(new { success = true, message = $"Visit number {0} is updated.", record.VisitId });
            }
            else
            {
                return Json(new { success = false, message = "Can not located this visit record." });
            }

        }

        [HttpPost]
        public IActionResult FlagVisit(int visitId, bool flag)
        {
            var visit = _urgentCareContext.Visit.FirstOrDefault(x => x.VisitId == visitId);

            if (visit != null)
            {
                visit.Flagged = flag;
                
                //remove flag will remove all flag rules
                if(!flag)
                {
                    var removed = _urgentCareContext.VisitRuleSet.Where(x => x.VisitId == visitId);
                    if(removed.Any())
                    {
                        _urgentCareContext.VisitRuleSet.RemoveRange(removed);
        
                    }
                }
                try
                {
                    _urgentCareContext.SaveChanges();


                    return Json(new { result = true });
                }
                catch
                {
                    return Json(new { success = false, message = "Can not flag this visit record." });
                }
            }
            else
            {
                return Json(new { success = false, message = "Can not located this visit record." });
            }
        }

        [HttpGet]
        public IActionResult GetFlaggedVisit(int? page, int? limit, string clinic, string physician, string rule, string finclass, DateTime startDate, DateTime endDate)
        {
            try
            {
                var records = _urgentCareContext.Visit.Where(x => x.Flagged)
               .Select(y => new VisitRecordVm
               {
                   VisitId = y.VisitId,
                   PatientId = y.PvPatientId,
                   ClinicName = y.ClinicId,
                   DiagCode = y.DiagCodes.Replace("|", "<br/>"),
                   PvRecordId = y.PvlogNum,
                   VisitTime = y.ServiceDate.ToString(),
                   PatientName = y.PvPatient.FirstName + " " + y.PvPatient.LastName,
                   OfficeKey = y.Physican.OfficeKey,
                   PVFinClass = y.PayerInformation.FirstOrDefault().Class.ToString(),
                   IcdCodes = y.Icdcodes.Replace("|", "<br/>"),
                   Payment = y.CoPayAmount.GetValueOrDefault(),
                   ProcCodes = y.ProcCodes.Replace(",|", "<br/>").Replace("|", "<br/>"),
                   IsFlagged = y.Flagged,
                   PhysicanId = y.PhysicanId,
                   ServiceDate = y.ServiceDate

                }).OrderBy(x => x.VisitTime).ToList();



                if (!string.IsNullOrEmpty(clinic)) 
                {
                    var clinicids = clinic.Split(',').ToList();
                    records = records.Where(x => clinicids.Contains(x.ClinicName)).ToList();
                }

                if (!string.IsNullOrEmpty(physician))
                {
                    var physicians = physician.Split(',').Select(int.Parse).ToList();
                    records = records.Where(x => physicians.Contains(x.PhysicanId)).ToList();

                }

                if (!string.IsNullOrEmpty(finclass))
                {
                    var finclasses = finclass.Split(',').ToList();
                    records = records.Where(x => finclasses.Contains(x.PVFinClass)).ToList();
                }

                if (!string.IsNullOrEmpty(rule))
                {
                    var rules =rule.Split(',').Select(int.Parse).ToList();
                    var affecedVisits = _urgentCareContext.VisitRuleSet.Where(x => rules.Contains(x.RuleSetId)).Select(y=>y.VisitId).ToList();

                    records = records.Where(x => affecedVisits.Contains(x.VisitId)).ToList();
                }

                if(startDate != DateTime.MinValue)
                {
                    records = records.Where(x => x.ServiceDate >= startDate).ToList();
                }

                if(endDate != DateTime.MinValue)
                {
                    records = records.Where(x => x.ServiceDate <= endDate).ToList();
                }

                var total = records.Count();

                if (page.HasValue && limit.HasValue)
                {
                    var start = (page.Value - 1) * limit.Value;
                    records = records.Skip(start).Take(limit.Value).ToList();

                    foreach(var record in records)
                    {
                        var visitRules = _urgentCareContext.VisitRuleSet.Include(x => x.CodeReviewRuleSet).Where(x => x.VisitId == record.VisitId).Select(x=>x.CodeReviewRuleSet.RuleName);
                        record.AppliedRules = string.Join("<br/>", visitRules);
                    }
                }
                return Json(new { records, total });
            }
           catch (Exception ex)
            {
                return Json(new { succcess = false, message= ex.Message.ToString() });
            }
        }


        [HttpGet]

        [HttpGet]
        public IActionResult GetBulkVisit(int? page, int? limit, string clinic, string physician, string rule, string finclass, DateTime startDate, DateTime endDate)
        {
            try
            {

                var records = _urgentCareContext.BulkVisit.Where(x => x.Flagged)
               .Select(y => new VisitRecordVm
               {
                   VisitId = y.VisitId,
                   PatientId = y.PvPatientId,
                   ClinicName = y.ClinicId,
                   DiagCode = y.DiagCodes.Replace("|", "<br/>"),
                   PvRecordId = y.PvlogNum,
                   VisitTime = y.ServiceDate.ToString(),
                   PatientName = y.PatientName,
                   OfficeKey = y.OfficeKey.GetValueOrDefault(),
                   PVFinClass = y.FinClass,
                   IcdCodes = y.Icdcodes.Replace("|", "<br/>"),
                   Payment = y.CoPayAmount.GetValueOrDefault(),
                   ProcCodes = y.ProcCodes.Replace(",|", "<br/>").Replace("|", "<br/>"),
                   IsFlagged = y.Flagged,
                   PhysicanId = y.PhysicanId,
                   ServiceDate = y.ServiceDate,
                   Selected = y.Selected

               }).OrderBy(x => x.VisitTime).AsQueryable();



                if (!string.IsNullOrEmpty(clinic))
                {
                    var clinicids = clinic.Split(',').ToList();
                    records = records.Where(x => clinicids.Contains(x.ClinicName));
                }

                if (!string.IsNullOrEmpty(physician))
                {
                    var physicians = physician.Split(',').Select(int.Parse).ToList();
                    records = records.Where(x => physicians.Contains(x.PhysicanId));

                }

                if (!string.IsNullOrEmpty(finclass))
                {
                    var finclasses = finclass.Split(',').ToList();
                    records = records.Where(x => finclasses.Contains(x.PVFinClass));
                }

                if (!string.IsNullOrEmpty(rule))
                {
                    var rules = rule.Split(',').Select(int.Parse).ToList();
                    var affecedVisits = _urgentCareContext.VisitRuleSet.Where(x => rules.Contains(x.RuleSetId)).Select(y => y.VisitId).ToList();

                    records = records.Where(x => affecedVisits.Contains(x.VisitId));
                }

                if (startDate != DateTime.MinValue)
                {
                    records = records.Where(x => x.ServiceDate >= startDate);
                }

                if (endDate != DateTime.MinValue)
                {
                    records = records.Where(x => x.ServiceDate <= endDate);
                }

                var total = records.Count();

                if (page.HasValue && limit.HasValue)
                {
                    var start = (page.Value - 1) * limit.Value;
                    records = records.Skip(start).Take(limit.Value);

                    foreach (var record in records)
                    {
                        var visitRules = _urgentCareContext.VisitRuleSet.Include(x => x.CodeReviewRuleSet).Where(x => x.VisitId == record.VisitId).Select(x => x.CodeReviewRuleSet.RuleName);
                        record.AppliedRules = string.Join("<br/>", visitRules);
                    }
                }
                return Json(new { records, total });
            }
            catch (Exception ex)
            {
                return Json(new { succcess = false, message = ex.Message.ToString() });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RunBatch(DateTime startDate, DateTime endDate)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var isDevelopment = environment == EnvironmentName.Development;
            var allclinics = _urgentCareContext.Visit.Include(x => x.VisitImpotLog).Where(x => x.ServiceDate >= startDate && x.ServiceDate <= endDate && !x.Flagged && !x.VisitImpotLog.Any()).Select(x => x.ClinicId).ToList();
            var officekeys = string.Join(",", _urgentCareContext.ClinicProfile.Where(x => allclinics.Contains(x.ClinicId)).DistinctBy(x => x.OfficeKey).Select(x => x.OfficeKey).ToArray());
            using (var webClient = new HttpClient())
            {
                if (environment == EnvironmentName.Development)
                {
                    webClient.BaseAddress = new Uri("http://localhost:65094/");
                }
                else
                {
                    webClient.BaseAddress = new Uri("http://172.31.22.98/");
                }
                webClient.DefaultRequestHeaders.Accept.Clear();


                var querystring = $"officeKey={officekeys}&startTime={startDate}&endTime={endDate}";

                var response = await webClient.PostAsync("cumsapi/Default/ImportToAmd?" + querystring, null);

                await response.Content.ReadAsStringAsync();


                return Json(new { sucess = true });
            }
        }

        [HttpGet]
        public IActionResult GetModifiedRecord(int? page, int? limit, DateTime startDate, DateTime endDate)
        {
            var total = 0;
            if (startDate != DateTime.MinValue && endDate != DateTime.MinValue)
            {


                var visits = _urgentCareContext.Visit.Include(x => x.VisitImpotLog).Where(x => x.ServiceDate >= startDate && x.ServiceDate <= endDate && x.IsModified && !x.VisitImpotLog.Any()).ToList();
                var Keys = visits.Select(x => "{\"VisitId\":" + x.VisitId + "}").ToList();

                var records = _urgentCareContext.Audit.Where(x => Keys.Contains(x.KeyValues)).Select(x => new
                {

                    modifiedBy = x.ModifiedBy,
                    modefiedTime = x.ModifiedTime.ToString("yyyy-MM-dd HH:MM:ss"),
                    newValues = x.NewValues,
                    oldValues = x.OldValues,
                    id = x.KeyValues
                }).ToList();

                total = records.Count();

                if (page.HasValue && limit.HasValue)
                {
                    var start = (page.Value - 1) * limit.Value;
                    records = records.Skip(start).Take(limit.Value).ToList();
                }
                return Json(new { records, total });
            }
            else
            {
                var records = new Audit();


                return Json(new { records, total });
            }

        }

        [HttpGet]

        public IActionResult LoadImported(int? page, int? limit, string sortBy, string direction, string office, DateTime startDate, DateTime endDate)
        {

            IQueryable<VisitImpotLog> query;
            var total = 0;
            if (!string.IsNullOrEmpty(office) && startDate != DateTime.MinValue && endDate != DateTime.MinValue)
            {
                var officekeys = office.Split(',').ToList();
                query = _urgentCareContext.VisitImpotLog.Include(x => x.Visit).ThenInclude(x => x.Physican)
                    .Where(x => 
                    officekeys.Contains(x.Visit.Physican.OfficeKey.ToString())
                    && x.Visit.ServiceDate >= startDate
                    && x.Visit.ServiceDate <= endDate );
            }
            else
            {
                query = _urgentCareContext.VisitImpotLog.Take(0);
            }
            var records = query.Select(y => new VisitRecordVm()
            {
                VisitId = y.VisitId,
                PatientId = y.Visit.PvPatientId,
                ClinicName = y.Visit.ClinicId,
                PhysicianName = y.Visit.Physican.DisplayName,
                InsuranceName = y.Visit.PayerInformation.FirstOrDefault().Insurance.PrimaryName,             
                VisitTime = y.Visit.ServiceDate.ToShortDateString(),
                PatientName = y.Visit.PvPatient.FirstName + " " + y.Visit.PvPatient.LastName,
                OfficeKey = y.Visit.Physican.OfficeKey,
                ImportedDate = y.ImportedDate.ToString("MM/dd/yyyy HH:mm:ss"),
                ChargeImported = y.ChargeImported != null? "Yes":"No",
                PatDocImported = y.Visit.PatientDocument.Any(x=> !string.IsNullOrEmpty(x.AmdFileId))?"Yes":"NO",
                PatChartImported = _urgentCareContext.ChartImportLog.Any(x=> x.PvChartDocId == y.Visit.ChartId)?"Yes":"No"
                
            }).OrderBy(x=>x.ImportedDate).ToList();

            total = records.Count();

            if (page.HasValue && limit.HasValue)
            {
                var start = (page.Value - 1) * limit.Value;
                records =  records.Skip(start).Take(limit.Value).ToList();
            }
            return Json(new { records, total });
        }

        [HttpGet]
        public async Task<IActionResult> GetHistoryCode(int? page, int? limit, int visitId, string type)
        {
            var total = 0;
            IEnumerable<Code> records = new List<Code>();
            if (_urgentCareContext.Visit.Any(x => x.VisitId == visitId))
            {
                var visitHistoryId = _urgentCareContext.Visit.Include(x => x.VisitHistory).FirstOrDefault(x => x.VisitId == visitId).VisitHistory.FirstOrDefault(x => !x.Saved).VisitHistoryId;
                var history = _urgentCareContext.VisitCodeHistory
                    .Where(x => x.VisitHistoryId == visitHistoryId && x.CodeType.Contains(type)).Select(x => new Code
                    {
                        Id = x.VisitCodeHistoryId,
                        CodeName = x.Code,
                        ModifierCode = x.Modifier,
                        ModifierCode2 = x.Modifier2,
                        CodeType = x.CodeType,
                        Quantity = x.Quantity.GetValueOrDefault(1),
                        ShortDescription = ""
                    }).ToList();

                records = from record in history
                          join lookupCode in _urgentCareContext.CptCodeLookup on record.CodeName equals lookupCode.Code into gj
                          from subCode in gj.DefaultIfEmpty()
                          select new Code
                          {
                              Id = record.Id,
                              CodeName = record.CodeName,
                              ModifierCode = record.ModifierCode,
                              ModifierCode2 = record.ModifierCode2,
                              CodeType = record.CodeType,
                              Quantity = record.Quantity,
                              ShortDescription = subCode?.ShortDescription ?? string.Empty
                          };
                records = records.ToList();
                total = records.Count();
            }

            if (type == "Icd")
            {
                using (var webClient = new HttpClient())
                {
                    var requestUri = new Uri("https://clinicaltables.nlm.nih.gov/api/icd10cm/v3/search?");
                    //webClient.BaseAddress = new Uri("https://clinicaltables.nlm.nih.gov/api/icd10cm/v3/search?");

                    webClient.DefaultRequestHeaders.Accept.Clear();

                    foreach (var icd in records)
                    {
                        var querystring = $"terms={icd.CodeName}";

                        var response = await webClient.PostAsync(requestUri + querystring, null);

                        var returnjson = await response.Content.ReadAsStringAsync();
                        var obj = JArray.Parse(returnjson);
                        if (obj!=null && obj[0].ToString() != "0" &&  obj[3][0][1] != null )
                        {
                            var output = obj[3][0][1].ToString();
                            records.FirstOrDefault(x => x.Id == icd.Id).Description = output;
                        }      
                    }
                }

            }

            return Json(new { records, total });
        }

        [HttpPost]
        public async Task<IActionResult> UploadChart(List<IFormFile> files, int visitId)
        {
            long size = files.Sum(f => f.Length);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    //first get the contantType right
                    if (ValidateContentType(formFile.ContentType))
                    {
                        if (size < 5120000)
                        {
                            using (var stream = new MemoryStream())
                            {
                                var fileName = formFile.FileName;
                                await formFile.CopyToAsync(stream);

                                var image = stream.ToArray();

                                var historyId = InitiatHistory(visitId);

                                try
                                {
                                    if (historyId != 0)
                                    {
                                        var tempcharts = _urgentCareContext.ChartDocumentHistory.Where(x => x.IsTemp && x.VisitHistoryId == historyId).ToArray();

                                        _urgentCareContext.ChartDocumentHistory.RemoveRange(tempcharts);

                                        _urgentCareContext.ChartDocumentHistory.Add(new ChartDocumentHistory
                                        {
                                            VisitHistoryId = historyId,
                                            UploadedBy = HttpContext.User.Identity.Name,
                                            UploadedTime = DateTime.Now,
                                            ChartImage = image,
                                            Filename = System.IO.Path.GetFileName(fileName),
                                            IsTemp = true

                                        });
                                        _urgentCareContext.SaveChanges();
                                        var base64 = Convert.ToBase64String(image);
                                        return Json(string.Format("data:image/jpeg;base64,{0}", base64));
                                    }

                                }
                                catch (Exception ex)
                                {
                                    ex.ToString();
                                }

                            }
                        }
                        else
                        {
                            return Json(new { error = "file is too large to uplaod. the maximum size of a file is 5 MB." });
                        }

                    }
                    else
                    {
                        return Json(new { error = "Invalid file content type, only gif, jpeg, tiff and png fiels are allowed." });
                    }

                }
            }
            return Json(new { error = "Service side error, can not upload this file right now." });
        }

        [HttpGet]
        public IActionResult GetChartHistoryImage(int charthisId)
        {
            var chart = _urgentCareContext.ChartDocumentHistory.FirstOrDefault(x => x.ChartDocumentHistoryId == charthisId);
            if (chart != null)
            {
                var base64 = Convert.ToBase64String(chart.ChartImage);
                return Json(string.Format("data:image/jpeg;base64,{0}", base64));
            }

            return null;
        }

        [HttpPost]
        public IActionResult AddHistoryCode(int visitId, string codeType, string code, int quantity, string modifier, string modifier2, string action)
        { var visitHistoryId = InitiatHistory(visitId);

            if (visitHistoryId > 0)
            {
                if (_urgentCareContext.VisitCodeHistory.Any(x => x.VisitHistoryId == visitHistoryId && x.CodeType == "EM_CPTCode") && codeType == "EM_CPTCode")
                {
                    return Json(new { success = false, responseText = "EM_CPTCode already exists, the code was not added. " });
                }
                _urgentCareContext.VisitCodeHistory.Add(new VisitCodeHistory {
                    VisitHistoryId = visitHistoryId,
                    Code = code,
                    CodeType = codeType,
                    Quantity = quantity,
                    Modifier = modifier,
                    Modifier2 = modifier2,
                    ModifiedBy = HttpContext.User.Identity.Name,
                    ModifiedTime = DateTime.UtcNow,
                    Action = action
                });

                try
                {
                    _urgentCareContext.SaveChanges();
                    return Json(new { success = true, responseText = "The Icd code was added." });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, responseText = "The Icd code was not added successfully!" });
                }
            }
            return Json(new { success = false, responseText = "The Icd code was not added." });
        }

        [HttpDelete]
        public IActionResult DeleteHistoryCode(int visitCodeHistoryId)
        {
            var code = new VisitCodeHistory { VisitCodeHistoryId = visitCodeHistoryId };
            _urgentCareContext.VisitCodeHistory.Attach(code);
            _urgentCareContext.VisitCodeHistory.Remove(code);
            try
            {
                _urgentCareContext.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }

        }

        [HttpPost]
        public IActionResult UpdateHistoryCode(Code record)
        {
            VisitCodeHistory historyCode;
            if (record.Id > 0)
            {
                historyCode = _urgentCareContext.VisitCodeHistory.First(x => x.VisitCodeHistoryId == record.Id);
                historyCode.Code = record.CodeName;
                historyCode.ModifiedBy = HttpContext.User.Identity.Name;
                historyCode.ModifiedTime = DateTime.UtcNow;
                historyCode.Modifier = record.ModifierCode;
                historyCode.Modifier2 = record.ModifierCode2;
                historyCode.Quantity = record.Quantity;
            }

            try
            {
                _urgentCareContext.SaveChanges();

                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveChartCode(int visitId, bool flaged)
        {
            if (visitId > 0)
            {
                var visit = _urgentCareContext.Visit.Include(x => x.VisitHistory).Include(x => x.Chart).ThenInclude(y => y.ChartDocument).First(x => x.VisitId == visitId);
                var visitHistory = visit.VisitHistory.First(x => !x.Saved);
                var visitHistoryCode = _urgentCareContext.VisitCodeHistory.Where(x => x.VisitHistoryId == visitHistory.VisitHistoryId && x.Action != "Saved");
                var document = visit.Chart.ChartDocument.FirstOrDefault();
                var visitHistoryDocument = _urgentCareContext.ChartDocumentHistory.FirstOrDefault(x => x.VisitHistoryId == visitHistory.VisitHistoryId && x.IsTemp);
                var oldProcCodes = _urgentCareContext.VisitProcCode.Where(x => x.VisitId == visitId).ToArray();
                var updatedDocName = string.Empty;
                var UpdatedDocImage = new byte[0];

                var newIcdCodes = _urgentCareContext.VisitCodeHistory.Where(x => x.VisitHistoryId == visitHistory.VisitHistoryId && x.CodeType == "IcdCode" && x.Action != "Modified").Select(x => x.Code).ToList();
                var newProcCodes = _urgentCareContext.VisitCodeHistory.Where(x => x.VisitHistoryId == visitHistory.VisitHistoryId && x.CodeType == "CPTCode" && x.Action != "Modified").ToList();
                var newEMCode = _urgentCareContext.VisitCodeHistory.Where(x => x.VisitHistoryId == visitHistory.VisitHistoryId && x.CodeType == "EM_CPTCode" && x.Action != "Modified").FirstOrDefault();

                visitHistory.FinalizedTime = DateTime.UtcNow;
                visitHistory.Saved = true;
                visitHistory.Icdcodes = visit.Icdcodes;
                visitHistory.Emcode = visit.Emcode;
                visitHistory.ProcCodes = visit.ProcCodes;
                visitHistory.ProcQty = visit.ProcQty;
                visitHistory.DiagCodes = visit.DiagCodes;

                if (visitHistoryDocument != null)
                {
                    updatedDocName = visitHistoryDocument.Filename;
                    UpdatedDocImage = visitHistoryDocument.ChartImage;
                    visitHistoryDocument.Filename = document.FileName;
                    visitHistoryDocument.ChartImage = document.DocumentImage;
                    visitHistoryDocument.IsTemp = false;
                }


                _urgentCareContext.VisitProcCode.RemoveRange(oldProcCodes);

                foreach (var proc in newProcCodes)
                {
                    var modifiercodes = string.Empty;
                    var procCode = proc.Code.Trim();
                    if (!string.IsNullOrEmpty(proc.Modifier) )
                    {
                        modifiercodes += proc.Modifier;
                    }

                    if (!string.IsNullOrEmpty(proc.Modifier2))
                    {
                        modifiercodes += "," + proc.Modifier2;
                    }

                    _urgentCareContext.VisitProcCode.Add(new VisitProcCode
                    {
                        VisitId = visitId,
                        Quantity = proc.Quantity,
                        ProcCode = procCode,
                        Modifier = modifiercodes
                    });
                }

                try
                {
                    await _urgentCareContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    //email
                    ex.ToString();
                    return Json(new { result = false });

                }

                var fullProcCode = string.Empty;
                foreach (var newProc in newProcCodes)
                {
                    var fullCode = newProc.Code.TrimEnd();
                    if (newProc.Quantity != 0)
                    {
                        fullCode = string.Format("{0},{1}", newProc.Quantity, fullCode);
                    }
                    if (!string.IsNullOrEmpty(newProc.Modifier))
                    {
                        fullCode = string.Format("{0},{1}", fullCode, newProc.Modifier);
                    }
                    if (!string.IsNullOrEmpty(newProc.Modifier2))
                    {
                        fullCode = string.Format("{0},{1}", fullCode, newProc.Modifier2);
                    }
                    if (newProc != newProcCodes.Last())
                    {
                        fullProcCode += fullCode + "|";
                    }
                    else
                    {
                        fullProcCode += fullCode;
                    }
                }

                if(newEMCode !=null)
                {
                    var emModifer = string.Empty;

                    if (!string.IsNullOrEmpty(newEMCode.Modifier))
                    {
                        emModifer = newEMCode.Modifier;
                    }
                    if (!string.IsNullOrEmpty(newEMCode.Modifier2))
                    {
                        emModifer = string.Format("{0},{1}", newEMCode.Modifier, newEMCode.Modifier2);
                    }

                    visit.IsModified = true;
                    visit.Emcode = newEMCode.Code;
                    visit.EmModifier = newEMCode.Modifier + ",";
                    visit.EmQuantity = newEMCode.Quantity;
                    visit.ProcCodes = fullProcCode;
                    visit.ProcQty = newProcCodes.Count();
                    visit.Flagged = flaged;
                }
                else
                {
                    visit.IsModified = true;
                    visit.Emcode = string.Empty;
                    visit.EmModifier = string.Empty;
                    visit.EmQuantity = null;
                }

                if (visitHistoryDocument != null)
                {
                    document.FileName = updatedDocName;
                    document.DocumentImage = UpdatedDocImage;
                }

                visit.Icdcodes = newIcdCodes.Aggregate((current, next) => $"{current}|{next}");

                try
                {
                    await _urgentCareContext.SaveChangesAsyncWithAudit(HttpContext.User.Identity.Name);
                }
                catch (Exception ex)
                {
                    //email 
                    ex.ToString();
                }

                return Json(new { sucess = true });
            }

            return Json(new { result = false });
        }

        [HttpPost]
        public async Task<IActionResult> SendNotification(string fromEmail, string toEmail, string subject, string body, List<IFormFile> files, int visitId, bool defaultChart)
        {
            if(visitId != 0 && defaultChart)
            {
                var image = new byte[0];
                var fileName = string.Empty;
                var visitHistory= _urgentCareContext.VisitHistory.First(x => x.VisitId == visitId);
                var visitChartHistory = _urgentCareContext.ChartDocumentHistory.FirstOrDefault(x => x.VisitHistoryId == visitHistory.VisitHistoryId);
                if(visitChartHistory!= null && visitChartHistory.IsTemp)
                {
                    image = visitChartHistory.ChartImage;
                    fileName = visitChartHistory.Filename;
                }
                else
                {
                    var currentVisit = _urgentCareContext.Visit.Include(x => x.Chart).ThenInclude(x => x.ChartDocument).First(x => x.VisitId == visitId);
                    image = currentVisit.Chart.ChartDocument.First().DocumentImage;
                    fileName = currentVisit.Chart.ChartDocument.First().FileName;
                }
                await _emailSrv.SendEmailAsync(fromEmail, toEmail, subject, body, image, fileName);
            }
            else
            {
                long size = files.Sum(f => f.Length);

                foreach (var formFile in files)
                {
                    if (formFile.Length > 0)
                    {
                        //first get the contantType right
                        if (ValidateContentType(formFile.ContentType))
                        {
                            if (size < 5120000)
                            {
                                using (var stream = new MemoryStream())
                                {
                                    var fileName = formFile.FileName;
                                    await formFile.CopyToAsync(stream);

                                    var image = stream.ToArray();

                                    await _emailSrv.SendEmailAsync(fromEmail, toEmail, subject, body, image, fileName);
                                    return Json(new { sucess = true });

                                }

                            }
                        }
                        else
                        {
                            return Json(new { error = "file is too large to uplaod. the maximum size of a file is 5 MB." });
                        }

                    }
                    else
                    {
                        return Json(new { error = "Invalid file content type, only gif, jpeg, tiff and png fiels are allowed." });
                    }

                }
            }
           
            return Json(new { error = "email did not send" });
        }
               
        [HttpPost]
        public IActionResult ScrubRecord(string officekey, DateTime startDate, DateTime endDate)
        {
          
            var officekeys = officekey.Split(',').ToList();
            var activeRules = _urgentCareContext.CodeReviewRule.Where(x => x.Active);
            var baseQuery = _urgentCareContext.Visit.Where(x => officekeys.Contains(x.OfficeKey.ToString()) && x.ServiceDate >= startDate && x.ServiceDate <= endDate && !x.Flagged && !x.VisitImpotLog.Any());
            var operationHelper = new OperationHelper();
            var results = new List<Visit>();
            var problemRuleNames = new List<string>();
            foreach (var ruleSet in activeRules)
            {
                var ruleDetail = !string.IsNullOrEmpty(ruleSet.RuleJsonString)
                    ? JsonConvert.DeserializeObject<List<RuleItem>>(ruleSet.RuleJsonString)
                    : new List<RuleItem>();
                var records = new List<Visit>();

                var ruleError = false;
                
                
                if (ruleDetail.Any())
                {
                    var filter = new Filter<Visit>();
                    
                    foreach (var item in ruleDetail)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(item.Openparenthese))
                            {
                                var groups = item.Openparenthese.ToCharArray().Count(a => a == '(');
                                filter.StartGroup();
                            }

                            var negation = true;
                            // this means the field is collectable 
                            if (item.Field.Contains("[") && item.Field.Contains("[") && item.Operator.ToLower() == "DoesNotContain".ToLower())
                            {
                                item.Operator = "Contains";
                                negation = false;
                            }

                            if (item.Field.Contains("[") && item.Field.Contains("[") && item.Operator.ToLower() == "NotEqualTo".ToLower())
                            {
                                item.Operator = "EqualTo";
                                negation = false;
                            }

                            if (item.LogicOperator != null)
                            {
                                var connector = (Connector)Enum.Parse(typeof(Connector), item.LogicOperator);
                                filter.By(item.Field, operationHelper.GetOperationByName(item.Operator), item.FieldValue, connector, negation);
                            }
                            else
                            {
                                filter.By(item.Field, operationHelper.GetOperationByName(item.Operator), item.FieldValue, negation);
                            }
                        }
                        catch
                        {
                            ruleError = true;
                            problemRuleNames.Add(ruleSet.RuleName);
                            break;
                        }
                       
                        
                    }
                    try
                    {
                        if(!ruleError)
                        {
                            records = baseQuery.Where(filter).ToList();
                        }
                                              
                    }
                    catch
                    {
                        return Json(new { success = false, message = "Failed to apply the rules, please try again." });
                    }
                }
                results = results.Union(records).ToList();
                try
                {
                    if (results.Any())
                    {
                        foreach (var result in results)
                        {
                            _urgentCareContext.Visit.FirstOrDefault(x => x.VisitId == result.VisitId).Flagged = true;
                            if (!_urgentCareContext.VisitRuleSet.Any(x => x.VisitId == result.VisitId && x.VisitRuleId == ruleSet.Id))
                            {
                                _urgentCareContext.VisitRuleSet.Add(new VisitRuleSet()
                                {

                                    VisitId = result.VisitId,
                                    RuleSetId = ruleSet.Id
                                });
                            }
                        }
                    }
                }

                catch(Exception ex)
                {
                    ex.ToString();
                }
                // if the ruleset found any record.. then mark them
               
            } 
            
            if(results.Any())
            {
                try
                {
                    
                    _urgentCareContext.SaveChanges();
                    var resultMessage = string.Empty;
                    if(problemRuleNames.Any())
                    {                      
                        string ruleNames = string.Empty;
                        if (problemRuleNames.Count()>1)
                        {
                            string dilimiter = ", ";
                            ruleNames = problemRuleNames.Aggregate((i, j) => i + dilimiter + j);
                        }
                        else
                        {
                            ruleNames = problemRuleNames[0];
                        }
                        resultMessage = string.Format("Rules found results, how ever {0} had problems to run, please check the definition. ", ruleNames);
                    }
                    return Json(new { success = true, message = resultMessage });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
            else
            {
                return Json(new { success = false, message = "This rule does not find any record that matches the conditions." });
            }
          
        }



        [HttpGet]
        public IActionResult BulkUpdate(int? page, int? limit)
        {      
            var records = _urgentCareContext.Visit.Include(x=>x.PvPatient).Include(x => x.PayerInformation).Include(x => x.Physican).Include(x=>x.AppliedRules)
                                
                .Where(x => x.Flagged).ToList();

            var vm = new FilterRecord
            {
                Clinics = records.DistinctBy(x => x.ClinicId).Select(y => new SelectListItem
                {
                    Selected = false,
                    Text = y.ClinicId,
                    Value = y.ClinicId,
                }).OrderBy(r => r.Text),
                Physicians = records.DistinctBy(x => x.PhysicanId).Select(y => new SelectListItem
                {
                    Selected = false,
                    Text = y.Physican.DisplayName,
                    Value = y.PhysicanId.ToString(),
                }).OrderBy(r => r.Text),
                FinClasses = records.DistinctBy(x => x.PayerInformation.Select(y => y.Class)).Select(y => new SelectListItem
                {
                    Selected = false,
                    Text = y.PayerInformation.FirstOrDefault() != null ? y.PayerInformation.FirstOrDefault().Class.ToString() : "0",
                    Value = y.PayerInformation.FirstOrDefault() != null ? y.PayerInformation.FirstOrDefault().Class.ToString() : "0"
                }).DistinctBy(z => z.Value).OrderBy(r => int.Parse(r.Value)),

                Clinic =string.Empty, 
                FinClass = string.Empty,
                Physician = string.Empty,
                FlaggedRule = string.Empty
            };

            vm.AppliedRuleIds = _urgentCareContext.VisitRuleSet.Where(x => records.Select(y => y.VisitId).Contains(x.VisitId)).DistinctBy(z => z.VisitRuleId).Select(x => x.VisitRuleId).ToList();
            
            vm.FlaggedRules = _urgentCareContext.CodeReviewRule.Where(x => vm.AppliedRuleIds.Contains(x.Id)).Select(y => new SelectListItem
            {
                Selected = false,
                Text = y.RuleName,
                Value = y.Id.ToString()
            }).ToList();

            // after loading the filter items. copy the visit to bulkVisit table

            foreach(var record in records)
            {
                if (_urgentCareContext.BulkVisit.Find(record.VisitId) == null)
                    _urgentCareContext.BulkVisit.Add(new BulkVisit
                    {
                        VisitId = record.VisitId,
                        TimeIn = record.TimeIn,
                        TimeOut = record.TimeOut,
                        LastUpdateTime = record.LastUpdateTime,
                        ServiceDate = record.ServiceDate,
                        PhysicanId = record.PhysicanId,
                        ClinicId = record.ClinicId,
                        OfficeKey = record.OfficeKey,
                        ProcCodes = record.ProcCodes,
                        Emcode = record.Emcode,
                        Icdcodes = record.Icdcodes,
                        LastUpdateUser = record.LastUpdateUser,
                        Flagged = record.Flagged,
                        GuarantorPayerId = record.GuarantorPayerId,
                        PvlogNum = record.PvlogNum,
                        PvPatientId = record.PvPatientId,
                        FinClass = record.PayerInformation.FirstOrDefault() != null ? record.PayerInformation.FirstOrDefault().Class.ToString() : "0",
                        PatientName = record.PvPatient.FirstName + " " + record.PvPatient.LastName,
                        CoPayAmount = record.CoPayAmount,
                        DiagCodes = record.DiagCodes,
                        EmModifier = record.EmModifier,
                        EmQuantity = record.EmQuantity,
                        ProcQty = record.ProcQty,
                        VisitType = record.VisitType,
                        SourceProcessId = record.SourceProcessId

                    });
                else
                {
                    var updateBulkVisit = _urgentCareContext.BulkVisit.Find(record.VisitId);
                    updateBulkVisit.VisitId = record.VisitId;
                    updateBulkVisit.TimeIn = record.TimeIn;
                    updateBulkVisit.TimeOut = record.TimeOut;
                    updateBulkVisit.LastUpdateTime = record.LastUpdateTime;
                    updateBulkVisit.ServiceDate = record.ServiceDate;
                    updateBulkVisit.PhysicanId = record.PhysicanId;
                    updateBulkVisit.ClinicId = record.ClinicId;
                    updateBulkVisit.OfficeKey = record.OfficeKey;
                    updateBulkVisit.ProcCodes = record.ProcCodes;
                    updateBulkVisit.Emcode = record.Emcode;
                    updateBulkVisit.Icdcodes = record.Icdcodes;
                    updateBulkVisit.LastUpdateUser = record.LastUpdateUser;
                    updateBulkVisit.Flagged = record.Flagged;
                    updateBulkVisit.GuarantorPayerId = record.GuarantorPayerId;
                    updateBulkVisit.PvlogNum = record.PvlogNum;
                    updateBulkVisit.PvPatientId = record.PvPatientId;
                    updateBulkVisit.FinClass = record.PayerInformation.FirstOrDefault() != null ? record.PayerInformation.FirstOrDefault().Class.ToString() : "0";
                    updateBulkVisit.PatientName = record.PvPatient.FirstName + " " + record.PvPatient.LastName;
                    updateBulkVisit.CoPayAmount = record.CoPayAmount;
                    updateBulkVisit.DiagCodes = record.DiagCodes;
                    updateBulkVisit.EmModifier = record.EmModifier;
                    updateBulkVisit.EmQuantity = record.EmQuantity;
                    updateBulkVisit.ProcQty = record.ProcQty;
                    updateBulkVisit.VisitType = record.VisitType;
                    updateBulkVisit.SourceProcessId = record.SourceProcessId;
                    _urgentCareContext.BulkVisit.Attach(updateBulkVisit);
                    _urgentCareContext.Entry(updateBulkVisit).State = EntityState.Modified;
                }
            }
           
            _urgentCareContext.SaveChanges();

            return View("BulkUpdateView", vm);
        }


        [HttpPost]

        public async Task<IActionResult> MarkBulkUpdate(int? page, int? limit, string clinic, string physician, string rule, string finclass, DateTime startDate, DateTime endDate, bool ischecked)
        {
            var records = _urgentCareContext.BulkVisit.AsQueryable();
            
            if (!string.IsNullOrEmpty(clinic))
            {
                var clinicids = clinic.Split(',').ToList();
                records = records.Where(x => clinicids.Contains(x.ClinicId));
            }

            if (!string.IsNullOrEmpty(physician))
            {
                var physicians = physician.Split(',').Select(int.Parse).ToList();
                records = records.Where(x => physicians.Contains(x.PhysicanId));

            }

            if (!string.IsNullOrEmpty(finclass))
            {
                var finclasses = finclass.Split(',').ToList();
                records = records.Where(x => finclasses.Contains(x.FinClass));
            }

            if (!string.IsNullOrEmpty(rule))
            {
                var rules = rule.Split(',').Select(int.Parse).ToList();
                var affecedVisits = _urgentCareContext.VisitRuleSet.Where(x => rules.Contains(x.RuleSetId)).Select(y => y.VisitId).ToList();

                records = records.Where(x => affecedVisits.Contains(x.VisitId));
            }

            if (startDate != DateTime.MinValue)
            {
                records = records.Where(x => x.ServiceDate >= startDate);
            }

            if (endDate != DateTime.MinValue)
            {
                records = records.Where(x => x.ServiceDate <= endDate);
            }

            await records.ForEachAsync(x => x.Selected = ischecked);
            try
            {
                await _urgentCareContext.SaveChangesAsync();
            }
            catch 
            {
                return Json(new { success = false });
            }

            return Json(new { success = true });

        }

        private IEnumerable<SelectListItem> GetAvaliableOfficeKeys()
        {
            return _urgentCareContext.ProgramConfig.Where(x => !x.AmdSync).DistinctBy(x => x.AmdofficeKey)
                 .Select(y =>
                      new SelectListItem
                      {
                          Selected = false,
                          Text = y.AmdofficeKey.ToString(),
                          Value = y.AmdofficeKey.ToString()
                      });

        }

        private bool ValidateContentType(string type)
        {
            var result = false;
            if (!string.IsNullOrEmpty(type))
            {
                if (type == "image/gif" || type == "image/png" || type == "application/pdf" || type == "image/jpeg" || type == "image/tiff")
                {
                    result = true;
                }
            }
            return result;
        }

        private int InitiatHistory(int visitId)
        {
            if (_urgentCareContext.Visit.FirstOrDefault(x => x.VisitId == visitId) != null)
            {
                if (_urgentCareContext.Visit.Include(x => x.VisitHistory).FirstOrDefault(x => x.VisitId == visitId).VisitHistory.Any(x => !x.Saved))
                {
                    return _urgentCareContext.Visit.FirstOrDefault(x => x.VisitId == visitId).VisitHistory.FirstOrDefault(x => !x.Saved).VisitHistoryId;
                }
                else
                {
                    var visitrec = _urgentCareContext.Visit.FirstOrDefault(x => x.VisitId == visitId);
                    _urgentCareContext.Visit.FirstOrDefault(x => x.VisitId == visitId).VisitHistory.Add(new VisitHistory
                    {
                        VisitId = visitId,
                        ServiceDate = visitrec.ServiceDate,
                        ModifiedBy = HttpContext.User.Identity.Name,
                        ModifiedTime = DateTime.UtcNow,
                        Saved = false
                    });
                    try
                    {
                        _urgentCareContext.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                        return 0;
                    }
                    return _urgentCareContext.Visit.FirstOrDefault(x => x.VisitId == visitId).VisitHistory.FirstOrDefault().VisitHistoryId;
                }

            }
            return 0; ;
        }
  
        
     
    }
}