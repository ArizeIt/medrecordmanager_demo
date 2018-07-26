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

        [HttpPost]
        public IActionResult LoadDaily(SearchInputs vm)
        {
            using (var db = new UrgentCareContext())
            {
                var records = db.Set<Visit>().ToList();/*Where(x => x.ClinicId == vm.Clinic && x.TimeIn >= vm.StartDate && x.TimeIn <= vm.EndDate);*/
            }

                var resultVm = new List<Dictionary<string, object>>()
            {
                new Dictionary<string, object>()
                    {
                        {"PatientName", "test"},
                        {"PvClinic","tEST" },
                        {"PVinClass", 20},
                        {"PD", 19745 },
                        {"ChiefDiag","1845" },
                        {"Codes", 19877},
                        {"OfficeKey", 2154846 },
                        {"VisitDate",DateTime.Now.ToShortDateString() },
                        {"Details" ,"SOME DATAILS"}
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