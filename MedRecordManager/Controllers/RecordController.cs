using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
using Newtonsoft.Json.Linq;
using PVAMCommon;
using UrgentCareData;
using UrgentCareData.Models;

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
        public IActionResult Review()
        {
            var vm = new SearchInputs()
            {
                Type = "Daily",

                OfficeKeys = GetAvaliableOfficeKeys()
            };
            return View("RecordView", vm);
        }

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
                records = records.Skip(start).Take(limit.Value).ToList();
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
                        vm.Chart.FileBinary = CreatePDF(vm.Chart.FileBinary);
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
                        _urgentCareContext.VisitCodeHistory.Add(new VisitCodeHistory
                        {
                            VisitHistoryId = visitHistoryId,
                            CodeType = "CPTCode",
                            Code = cpt.ProcCode,
                            Modifier = modifier1,
                            Modifier2 = modifier2,
                            Quantity = cpt.Quantity.GetValueOrDefault(),
                            ModifiedBy = HttpContext.User.Identity.Name,
                            ModifiedTime = DateTime.UtcNow,
                            Action = "Cloned"
                        });
                    }

                    var emcode = _urgentCareContext.Visit.Include(x => x.VisitProcCode).FirstOrDefault(x => x.VisitId == visit.VisitId).Emcode;

                    Utility.ParseCptCode(emcode, out string em, out int emQuantity, out string emModifier1, out string emModifier2);
                    
                    _urgentCareContext.VisitCodeHistory.Add(new VisitCodeHistory
                    {
                        VisitHistoryId = visitHistoryId,
                        CodeType = "EM_CPTCode",
                        Code = em,
                        Modifier = emModifier1,
                        Modifier2 = emModifier2,
                        Quantity = emQuantity,
                        ModifiedBy = HttpContext.User.Identity.Name,
                        ModifiedTime = DateTime.UtcNow,
                        Action = "Cloned"
                    });



                    _urgentCareContext.SaveChanges();
                }
            }
            return View("CodeView", vm);
        }

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
                records = records.Skip(start).Take(limit.Value).ToList();
            }


            return Json(new { records, total });
        }

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

        public async Task<IActionResult> GetClinics()
        {
            var records = await _urgentCareContext.ClinicProfile.Select(x => new { id = x.ClinicId, text = x.ClinicId }).ToListAsync();
            return Json(records);
        }

        public async Task<IActionResult> GetModifiers()
        {
            var records = await _urgentCareContext.Modifier.Select(x => new { id = x.ModifierCode, text = x.ModifierCode }).ToListAsync();
            records.Insert(0, new { id = "", text = "" });
            return Json(records);
        }


        public async Task<IActionResult> GetPhysicians(string clinicId)
        {
            var physicians = await _urgentCareContext.Physican.Where(x => x.Clinic == clinicId).Select(x => new { id = x.PvPhysicanId, text = x.PvPhysicanId }).ToListAsync();
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

        public IActionResult GetFlaggedVisit(int? page, int? limit)
        {
            var records = _urgentCareContext.Visit.Where(x => x.Flagged)
                .Select(y => new VisitRecordVm {
                    VisitId = y.VisitId,
                    PatientId = y.PvPatientId,
                    ClinicName = y.ClinicId,
                    DiagCode = y.DiagCodes.Replace("|", "<br/>"),
                    PvRecordId = y.PvlogNum,
                    VisitTime = y.ServiceDate.ToShortDateString(),
                    PatientName = y.PvPatient.FirstName + " " + y.PvPatient.LastName,
                    OfficeKey = y.Physican.OfficeKey,
                    PVFinClass = y.PayerInformation.FirstOrDefault().Class.ToString(),
                    IcdCodes = y.Icdcodes.Replace("|", "<br/>"),
                    Payment = y.CoPayAmount.GetValueOrDefault(),
                    ProcCodes = y.ProcCodes.Replace(",|", "<br/>").Replace("|", "<br/>"),
                    IsFlagged = y.Flagged

                }).ToList();

            var total = records.Count();

            if (page.HasValue && limit.HasValue)
            {
                var start = (page.Value - 1) * limit.Value;
                records = records.Skip(start).Take(limit.Value).ToList();
            }
            return Json(new { records, total });
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
                        Quantity = x.Quantity.GetValueOrDefault(0),
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
                        var output = obj[3][0][1].ToString();
                        records.FirstOrDefault(x => x.Id == icd.Id).Description = output;
                    }


                }

            }

            return Json(new { records, total });
        }


        //public async Task<IActionResult> GetModifierCode()
        //{
        //    var records = await _urgentCareContext.Modifier.Select(x => new { id = x.ModifierCode, text = x.ModifierCode }).ToListAsync();
        //    records.Insert(0, new { id = "", text = "" });
        //    return Json(records);
        //}

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
                                            FileName = System.IO.Path.GetFileName(fileName),
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
                if (_urgentCareContext.VisitCodeHistory.Any(x => x.VisitHistoryId == visitHistoryId && x.CodeType == "EM_CPTCode"))
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
                var newEMCodes = _urgentCareContext.VisitCodeHistory.Where(x => x.VisitHistoryId == visitHistory.VisitHistoryId && x.CodeType == "EM_CPTCode" && x.Action != "Modified").ToList();

                visitHistory.FinalizedTime = DateTime.UtcNow;
                visitHistory.Saved = true;
                visitHistory.Icdcodes = visit.Icdcodes;
                visitHistory.Emcode = visit.Emcode;
                visitHistory.ProcCodes = visit.ProcCodes;
                visitHistory.ProcQty = visit.ProcQty;
                visitHistory.DiagCodes = visit.DiagCodes;

                if (visitHistoryDocument != null)
                {
                    updatedDocName = visitHistoryDocument.FileName;
                    UpdatedDocImage = visitHistoryDocument.ChartImage;
                    visitHistoryDocument.FileName = document.FileName;
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


                var fullEmcode = string.Empty;
                var fullProcCode = string.Empty;
                foreach (var newEm in newEMCodes)
                {
                    var fullCode = newEm.Code.TrimEnd();
                    if (newEm.Quantity != 0)
                    {
                        fullCode = string.Format("{0},{1}", newEm.Quantity, fullCode);
                    }
                    if (!string.IsNullOrEmpty(newEm.Modifier))
                    {
                        fullCode = string.Format("{0},{1}", fullCode, newEm.Modifier);
                    }
                    if (!string.IsNullOrEmpty(newEm.Modifier2))
                    {
                        fullCode = string.Format("{0},{1}", fullCode, newEm.Modifier2);
                    }
                    if (newEm != newEMCodes.Last())
                    {
                        fullEmcode += fullCode + "|";
                    }
                    else
                    {
                        fullEmcode += fullCode;
                    }
                }

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


                visit.IsModified = true;
                visit.Icdcodes = newIcdCodes.Aggregate((current, next) => $"{current}|{next}");
                visit.Emcode = fullEmcode;
                visit.ProcCodes = fullProcCode;
                visit.ProcQty = newProcCodes.Count();
                visit.Flagged = flaged;

                if (visitHistoryDocument != null)
                {
                    document.FileName = updatedDocName;
                    document.DocumentImage = UpdatedDocImage;
                }


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
        public async Task<IActionResult> SendNotification(string fromEmail, string toEmail, string subject, string body, List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    //first get the contantType right
                    if (ValidateContentType(formFile.ContentType)) {
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
            return Json(new { error = "email did not send" });
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

        
        private void ConvertTiff2Jpeg(byte[] source, string jpegFileName)
        {
            var stream = new MemoryStream(source);
            var img = Image.FromStream(stream);
            var outputbyte = new byte[0];
            var count = img.GetFrameCount(FrameDimension.Page);
            for (int i = 0; i < count; i++)
            {
                using (var partialStream = new MemoryStream())
                {
                    img.SelectActiveFrame(FrameDimension.Page, i);
                    img.Save(partialStream, ImageFormat.Jpeg);
                    partialStream.ToArray();
                }
            }


            int imageWidth = img.Width;
            int imageHeight = img.Height * count;
            Bitmap joinedBitmap = new Bitmap(imageWidth, imageHeight);
            Graphics graphics = Graphics.FromImage(joinedBitmap);
            for (int i = 0; i < count; i++)
            {
                var partImageFileName = jpegFileName + ".part" + i + ".jpg";
                Image partImage = Image.FromFile(partImageFileName);
                graphics.DrawImage(partImage, 0, partImage.Height * i, partImage.Width, partImage.Height);
                partImage.Dispose();
            }

            joinedBitmap.Save(jpegFileName);
            graphics.Dispose();
            joinedBitmap.Dispose();
            img.Dispose();
        }


        private byte[] CreatePDF(byte[] source)
        {
            var baos = new ByteArrayOutputStream();
            var pdfDoc = new PdfDocument(new PdfWriter(baos));
            var document = new Document(pdfDoc);
            var pageCount = TiffImageData.GetNumberOfPages(source);

            for(int i = 1; i<= pageCount; i++ )
            {
                var tiffImage = ImageDataFactory.CreateTiff(source, true, i, true);
                var tiffPageSize = new iText.Kernel.Geom.Rectangle(tiffImage.GetWidth(), tiffImage.GetHeight());
                var newPage = pdfDoc.AddNewPage(new PageSize(tiffPageSize));
                var canvas = new PdfCanvas(newPage);
                canvas.AddImage(tiffImage, tiffPageSize, false);
            }
           
            document.Close();
            return baos.ToArray();
        }



        private Stream CreateCompressedImageStream(Image image)
        {
            MemoryStream imageStream = new MemoryStream();

            var info = ImageCodecInfo.GetImageEncoders().FirstOrDefault(i => i.MimeType.ToLower() == "image/png");
            EncoderParameter colorDepthParameter = new EncoderParameter(Encoder.ColorDepth, 1L);
            var parameters = new EncoderParameters(1);
            parameters.Param[0] = colorDepthParameter;

            image.Save(imageStream, info, parameters);

            imageStream.Position = 0;
            return imageStream;
        }
    }
}