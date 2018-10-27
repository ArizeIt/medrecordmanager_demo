using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MedRecordManager.Extension;
using MedRecordManager.Models;
using MedRecordManager.Models.DailyRecord;
using MedRecordManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        public RecordController(UrgentCareContext urgentData, IViewRenderService viewRenderService)
        {
             _urgentCareContext = urgentData;
            _viewRenderService = viewRenderService;
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
                query = _urgentCareContext.Visit.Include(x=>x.VisitImpotLog).Include(x=>x.Physican).Where(x => officekeys.Contains(x.Physican.OfficeKey.ToString()) && x.ServiceDate >= startDate && x.ServiceDate <= endDate && !x.VisitImpotLog.Any());
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
                VisitId = visitId.ToString()
            };

            var visitRec = _urgentCareContext.Visit.SingleOrDefault(x => x.VisitId == visitId);
            if(visitRec !=null)
            {
                detailRecord.GuarantorInfo = _urgentCareContext.GuarantorInformation.Where(x => x.PvPatientId == visitRec.PvPatientId).Select(g=> new Guarantor {
                    FirstName = g.FirstName,
                    LastName = g.LastName,

                }).ToList();

                detailRecord.ChartId = visitRec.ChartId.Value;

                detailRecord.VisitCharts = _urgentCareContext.ChartDocument.Where(x => x.ChartId == visitRec.ChartId).Select(c => new ChartDoc
                {
                    ChartDocId = c.ChartDocId,
                    FileName = c.FileName

                }).ToList();

            if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    _viewRenderService.RenderToStringAsync("DetailView", detailRecord);
                }
            }
            
            return PartialView("DetailView", detailRecord);

        }

        public async Task<IActionResult> GetClinics()
        {
            var clinics = await  _urgentCareContext.ClinicProfile.Select(x => new { id = x.ClinicId, text = x.ClinicId }).ToListAsync();
            return Json(clinics);
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
                else
                {
                    return Json(new { success = false, message = "Value is unchanged" });
                }
                return Json(new { success = true, record });
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

                var response = await webClient.PostAsync("api/Default/ImportToAmd?" + querystring, null);

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
                var Keys = visits.Select(x => "{\"VisitId\":" +x.VisitId +"}").ToList();
               
                var records = _urgentCareContext.Audit.Where(x=> Keys.Contains(x.KeyValues)).Select(x => new
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


        private IEnumerable<SelectListItem> GetAvaliableOfficeKeys()
        {
            return _urgentCareContext.ProgramConfig.Where(x=>! x.AmdSync).DistinctBy(x => x.AmdofficeKey)
                 .Select(y =>
                      new SelectListItem
                      {
                          Selected = false,
                          Text = y.AmdofficeKey.ToString(),
                          Value = y.AmdofficeKey.ToString()
                      });

        }
    }
}