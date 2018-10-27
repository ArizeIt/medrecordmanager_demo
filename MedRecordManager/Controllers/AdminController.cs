using System.Collections.Generic;
using System.Linq;
using MedRecordManager.Extension;
using MedRecordManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UrgentCareData;
using UrgentCareData.Models;

namespace MedRecordManager.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly UrgentCareContext _urgentData;

        private SearchInputs Input {get { return InitializeInput(); } }
        private SearchInputs InitializeInput ()
        {
            IEnumerable<SelectListItem> emptyRecord = new SelectListItem[]
          {

                new SelectListItem{
                    Selected = false,
                    Text = "-Select Office Key-",
                    Value = string.Empty
                }
          };


            var input = new SearchInputs
            {
                OfficeKeys = _urgentData.Set<ProgramConfig>().DistinctBy(x => x.AmdofficeKey).OrderBy(x => x.AmdofficeKey).Select(x =>
                  new SelectListItem
                  {
                      Selected = false,
                      Text = x.AmdofficeKey.ToString(),
                      Value = x.AmdofficeKey.ToString()
                  }
                    )
            };
            input.OfficeKeys = input.OfficeKeys.Concat(emptyRecord).OrderBy(x => x.Value);
            return input;
        }


        public AdminController(UrgentCareContext urgentContext)
        {
            _urgentData = urgentContext;

        }
        public IActionResult Physician()
        {
            return View("Physician", Input);
        }

        public IActionResult Clinic()
        {
            return null;
        }

        public IActionResult Rule ()
        {
          
            return View("RulePage", Input);
        }

        [HttpGet]
        public IActionResult getMapedPh(int officeKey)
        {
          
            if(officeKey!=0)
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
                    if (_urgentData.Set<Physican>().FirstOrDefault(x => x.PvPhysicanId == physician.pvPhysicianId && x.OfficeKey == physician.Inputs.OfficeKey) == null)
                    {
                        _urgentData.Set<Physican>().Add(new Physican
                        {
                            PvPhysicanId = physician.pvPhysicianId ?? default(int),
                            AmProviderId = physician.AmdDisplayName,
                            AmdCode = "NVFR",
                            FirstName = physician.pvFirstName,
                            LastName = physician.pvLastName,
                            IsDefault = physician.IsDefault,
                            OfficeKey = physician.Inputs.OfficeKey

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
        public IActionResult DeletePhyisican(int pvPhysicianId, int officeKey)
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