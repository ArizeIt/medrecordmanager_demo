using System;
using System.Collections.Generic;
using System.Linq;
using MedRecordManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UgentCareDate;
using UgentCareDate.Models;

namespace MedRecordManager.Controllers
{
    public class RecordController : Controller
    {
        private readonly UrgentCareContext _urgentCareContext;

        public RecordController(UrgentCareContext urgentData)
        {
             _urgentCareContext = urgentData;
        }
        public IActionResult Review()
        {
            
            var vm = new SearchInputs() {Type="Daily",

             OfficeKeys =  new List<SelectListItem> {
                new SelectListItem{Selected = false, Text="123456",Value="123456"},
                new SelectListItem{Selected = false, Text="3233456",Value="3233456"}
                }
        };

            return View("RecordView", vm);
        }

        public IActionResult Callback()
        {

            var vm = new SearchInputs()
            {
                Type = "Callback",
                OfficeKeys = new List<SelectListItem> {
                new SelectListItem{Selected = false, Text="1234567",Value="123456"},
                new SelectListItem{Selected = false, Text="3233456",Value="3233456"}
                },

                Clinics = new List<SelectListItem> {
                new SelectListItem{Selected = false, Text="Melissa",Value="123456"},
                new SelectListItem{Selected = false, Text="Highway285",Value="3233456"}
                },

            };
            return View("RecordView", vm);
        }

       

        public IActionResult LoadDaily(int? page, int? limit, string sortBy, string direction, string office, DateTime startDate, DateTime endDate)
        {
            var records = new List<VisitRecordVm>()
            {
                new VisitRecordVm()
                {
                    PatientName = "Test Patient",
                    ClinicName = "USHyw287",
                    DiagCode = "20",
                    OfficeKey = "1231456",
                    PatientId = 123456,
                    PVFinClass = "BS",
                    PvRecordId = 1346464,
                    VisitTime = DateTime.Now
                }
            };
            var total = records.Count();

            return Json(new { records, total });          
                
        }

  
        public IActionResult LoadCallback(int? page, int? limit, string sortBy, string direction, string office, DateTime startDate, DateTime endDate)
        {
            var records = new List<PatientVisitVM>
            {
               new PatientVisitVM()
               {
                   PatientName = "Test",
                   PvClinic = "USHYW287",
                   VisitDate = DateTime.Now.ToShortDateString(),
                   PvId = "123456", 
                   PvPhone = "21345779797",
                   PvPhoneType = "Home", 
                   CellPhone = "123454646"
               }
            };


            return Json(new { records, records.Count });
        }




    }
}