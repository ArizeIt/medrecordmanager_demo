using System.Collections.Generic;
using MedRecordManager.Models;
using MedRecordManager.Models.PhsycianRecord;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UgentCareDate;
using UgentCareDate.Models;

namespace MedRecordManager.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly UrgentCareContext _urgentData; 
        public AdminController(UrgentCareContext urgentContext)
        {
            _urgentData = urgentContext;

        }
        public IActionResult Physician()
        {
            var vm = new SearchInputs()
            {
                Type = "Daily",

                OfficeKeys = new List<SelectListItem> {
                new SelectListItem{Selected = false, Text="123456",Value="123456"},
                new SelectListItem{Selected = false, Text="3233456",Value="3233456"}
                }
            };
            return View("Physician", vm);
        }

        [HttpPost]
        public IActionResult getMapedPh(string OfficeKey)
        {
          
            var vm = new List<PhysicianVm>() {

                new PhysicianVm()
                {
                    pvPysicianId = 123456,
                    pvFirstName = "Test", 
                    pvLastName = "Tester"
                },

                 new PhysicianVm()
                {
                    pvPysicianId = 123456,
                    pvFirstName = "Test2",
                    pvLastName = "Tester2"
                }
            };
            return PartialView("_MappedPhysician", vm);
        }

        [HttpPost]
        public IActionResult AddPhysician(string officeKey)
        {
            
            var vm = new PhysicianVm {
                MappedProviders = new List<SelectListItem>()
                {
                    new SelectListItem
                    {
                        Value ="Prof2056",
                        Text = "Joe, Test"
                    },

                    new SelectListItem
                    {
                          Value ="Prof2057",
                        Text = "Jane, Test"
                    }
                }
            };
            return PartialView("_AddPhysician", vm);
        }

        [HttpPost]

        public IActionResult SavePhysician( PhysicianVm input)
        {

          if(ModelState.IsValid)
            {
                _urgentData.Set<Physican>().Add(new Physican
                {
                    PvPhysicanId = input.pvPysicianId,
                    AmProviderId = input.AmdDisplayName,
                    AmdCode ="NVFR",
                    FirstName = input.pvFirstName,
                    LastName = input.pvLastName,
                    Clinic = "NewClinic",
                    IsDefault = input.IsDefault,
                    OfficeKey = input.OfficeKey

                });
                _urgentData.SaveChanges();
            }

            return PartialView("_MappedPhysician");
        }
    }
}