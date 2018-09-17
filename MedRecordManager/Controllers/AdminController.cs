using System.Collections.Generic;
using System.Linq;
using MedRecordManager.Extension;
using MedRecordManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UgentCareDate;
using UgentCareDate.Models;
using UrgentCareData.Models;

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
            IEnumerable<SelectListItem> emptyRecord = new SelectListItem[]
            {

                new SelectListItem{
                    Selected = false,
                    Text = "-Select Office Key-",
                    Value = string.Empty
                }
            };
            var vm = new PhysicianVm()
            {
                Input = new SearchInputs
                {
                    OfficeKeys = _urgentData.Set<ClinicProfile>().DistinctBy(x => x.OfficeKey).OrderBy(x=> x.OfficeKey).Select(x =>
                     new SelectListItem
                     {
                         Selected = false,
                         Text = x.OfficeKey.ToString(),
                         Value = x.OfficeKey.ToString()
                     }
                    )
                }
            };

            vm.Input.OfficeKeys = vm.Input.OfficeKeys.Concat(emptyRecord).OrderBy(x => x.Value);
            return View("Physician", vm);
        }

        [HttpGet]
        public IActionResult getMapedPh(string officeKey)
        {
          
            if(!string.IsNullOrEmpty(officeKey))
            {
                var vm = new List<PhysicianVm>();
                vm = _urgentData.Set<Physican>().Where(x => x.OfficeKey == officeKey).Select(x => new PhysicianVm()
                {
                    AmdDisplayName = x.DisplayName,
                    AmdProfileId = x.AmProviderId,
                    AmdProviderCode = x.AmdCode,
                    pvFirstName = x.FirstName,
                    pvLastName = x.LastName,
                    pvPhysicianId = x.PvPhysicanId,
                }).ToList();
                return PartialView("_MappedPhysician", vm);
            }

            return null;
        }

        [HttpGet]
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
        public IActionResult SavePhysician(PhysicianVm physician)
        {

            if (ModelState.IsValid)
            {
                if (physician.pvPhysicianId.HasValue)
                {
                    if (_urgentData.Set<Physican>().FirstOrDefault(x => x.PvPhysicanId == physician.pvPhysicianId && x.OfficeKey == physician.Input.OfficeKey.ToString()) == null)
                    {
                        _urgentData.Set<Physican>().Add(new Physican
                        {
                            PvPhysicanId = physician.pvPhysicianId ?? default(int),
                            AmProviderId = physician.AmdDisplayName,
                            AmdCode = "NVFR",
                            FirstName = physician.pvFirstName,
                            LastName = physician.pvLastName,
                            IsDefault = physician.IsDefault,
                            OfficeKey = physician.Input.OfficeKey.ToString()

                        });
                        _urgentData.SaveChanges();
                    }
                    else
                    {
                        ModelState.AddModelError("Duplicate Physician", "This physican with the same Id and Office Key has already been mapped.");
                    }
                }
            }

            return PartialView("_MappedPhysician");
        }

        [HttpPost]
        public IActionResult DeletePhyisican(int pvPhysicianId, string officeKey)
        {
            var match = _urgentData.Set<Physican>().FirstOrDefault(x => x.PvPhysicanId == pvPhysicianId && x.OfficeKey == officeKey);
            if (match != null)
            {
                _urgentData.Set<Physican>().Remove(match);
                _urgentData.SaveChanges();
            }

            return getMapedPh(officeKey);
        }

    }
}