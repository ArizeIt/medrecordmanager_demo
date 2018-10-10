using System;
using System.Linq;
using MedRecordManager.Extension;
using MedRecordManager.Models;
using MedRecordManager.Models.DailyRecord;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UgentCareDate;
using UgentCareDate.Models;
using UrgentCareData.Models;

namespace MedRecordManager.Controllers
{
    [Authorize]
    public class RecordController : Controller
    {
        private readonly UrgentCareContext _urgentCareContext;

        public RecordController(UrgentCareContext urgentData)
        {
             _urgentCareContext = urgentData;
        }
        public IActionResult Review()
        {
            var vm = new SearchInputs()
            {
                Type = "Daily",

                OfficeKeys = _urgentCareContext.ProgramConfig.Where(x=> !x.AmdSync).DistinctBy(x => x.AmdofficeKey).Select(y =>
                    new SelectListItem
                    {
                        Selected = false,
                        Text = y.AmdofficeKey.ToString(),
                        Value = y.AmdofficeKey.ToString()
                    })
            };
            return View("RecordView", vm);
        }

        public IActionResult Callback()
        {

            var vm = new SearchInputs()
            {
                Type = "Callback",
                OfficeKeys = _urgentCareContext.ProgramConfig.Where(x => !x.AmdSync).DistinctBy(x => x.AmdofficeKey).Select(y =>
                     new SelectListItem
                     {
                         Selected = false,
                         Text = y.AmdofficeKey.ToString(),
                         Value = y.AmdofficeKey.ToString()
                     }),

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
            if (!string.IsNullOrEmpty(office) && startDate != DateTime.MinValue && endDate != DateTime.MinValue)
            {
                var officekeys = office.Split(',').Select(int.Parse).ToList();
                query = _urgentCareContext.Visit.Include(x=>x.ImportLog).Where(x => officekeys.Contains(x.ClinicProfile.OfficeKey) && x.ServiceDate >= startDate && x.ServiceDate <= endDate && x.ImportLog == null);
            }
            else
            {
                query = _urgentCareContext.Visit.Where(x=>x.ImportLog !=null && x.ImportLog.AmdimportLog == null);
            }
            var records = query.Select(y => new VisitRecordVm()
            {
                VisitId = y.VisitId,
                PatientId = y.PvPaitentId,
                ClinicName = y.ClinicId,
                DiagCode = y.DiagCodes.Replace("|", ","),
                PvRecordId = y.PvlogNum,
                VisitTime = y.ServiceDate.ToShortDateString(),
                PatientName = y.PvPaitent.FirstName + " " + y.PvPaitent.LastName,
                OfficeKey = y.ClinicProfile.OfficeKey.ToString(),
                PVFinClass = y.PayerInformation.FirstOrDefault().Class.ToString(),
                IcdCodes = y.Icdcodes.Replace("|", ","),
                Payment = y.CoPayAmount.GetValueOrDefault()
            }).ToList();

            var total = records.Count();

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
                PvId = y.PvPaitentId.ToString(),
                PvClinic = y.ClinicId,
               
                VisitDate = y.ServiceDate.ToShortDateString(),
                PatientName = y.PvPaitent.FirstName + " " + y.PvPaitent.LastName,
                PvPhone = y.PvPaitent.PatPhone
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
            IQueryable<PayerInformation> payerQuery;
            IQueryable<GuarantorInformation> GuarantorQuery;
            IQueryable<ChartDoc> ChartQuery;

            var detailRecord = new DetailRecord();

            return PartialView("DetailView", detailRecord);

        }

        public IActionResult GetClinic()
        {
            var clinics= _urgentCareContext.ClinicProfile.Select (x=>x.ClinicId);

            return Json(new {clinics});
        }


    }
}