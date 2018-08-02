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

        [HttpPost]
        public IActionResult Search(SearchInputs vm)
        {  if(vm.Type == "Daily")
            {
                return PartialView("_visitRecords");
            }
          if (vm.Type =="Callback")
            {
                return PartialView("_CallbackRecordView");
            }
            return null;
        }

        
        public IActionResult LoadDaily(int? page, int? limit, string sortBy, string direction, string office, DateTime startDate, DateTime endDate)
        {
            if(startDate> DateTime.MinValue)
            {
                var query = _urgentCareContext.Set<Visit>().Where(x => x.TimeIn > startDate).ToList();
                if(query.Count == 0)
                {
                    var result = new Visit
                    {
                        PvPaitent = new PatientInformation
                        {

                        }
                    };
                    return Json(new { })
                }
                return Json(new { query, query.Count });
            }

            return null;          
                
        }

        [HttpPost]
        public IActionResult LoadCallback(SearchInputs vm)
        {
            var resultVm = new List<Dictionary<string, object>>()
            {
                new Dictionary<string, object>()
                    {
                        {"PatientName", "test Patient"},
                        {"PvClinic","TEST1" },
                        {"VisitDate", DateTime.Now.ToShortDateString()},
                        {"PvId", 20 },
                        {"PvPhone","123-456-7891" },
                        {"PvPhoneType", "Cell"},
                        {"CellPhone", "123-456-7890" },
                    }
            };


            var datavm = new DataTableResponse
            {
                data = resultVm,
                recordsTotal = 1,
                draw = 1
            };
            return Json(datavm);
        }


    }

    internal class DtResponse
    {
        public List<Dictionary<string, object>> data { get; set; }
    }
}