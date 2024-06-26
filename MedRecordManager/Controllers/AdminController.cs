﻿using AdvancedMDDomain.DTOs.Responses;
using AdvancedMDInterface;
using MedRecordManager.Extension;
using MedRecordManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrgentCareData;
using UrgentCareData.Models;

namespace MedRecordManager.Controllers
{
    [Authorize]
    [ResponseCache(NoStore = true, Duration = 0)]
    public class AdminController : Controller
    {
        private readonly UrgentCareContext _urgentData;

        private readonly ILookupService _lookupService;

        private readonly ILoginService _apiLoginServicce;


        public AdminController(UrgentCareContext urgentData, ILookupService lookupService, ILoginService apiLoginServicce)
        {
            _urgentData = urgentData;
            _lookupService = lookupService;
            _apiLoginServicce = apiLoginServicce;
        }
        private SearchInputs InitializeInput()
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



        public IActionResult Physician()
        {

            var physican = new PhysicianVm
            {
                Inputs = InitializeInput()
            };

            return View("Physician", physican);
        }

        public IActionResult Clinic()
        {
            var mappedClinics = _urgentData.ClinicProfile.Where(x => !string.IsNullOrEmpty(x.OfficeKey)).Select(x =>

                 new ClinicVm
                 {
                     ClinicId = x.ClinicId,
                     OfficeKey = x.OfficeKey,
                     AmdFacility = x.AmdcodeName,

                 });
            return View("Clinic", mappedClinics);

        }


        [HttpGet]
        public IActionResult MappedClinic()
        {
            var mappedClinics = _urgentData.ClinicProfile.Where(x => !string.IsNullOrEmpty(x.OfficeKey)).Select(x =>

                 new ClinicVm
                 {
                     ClinicId = x.ClinicId,
                     OfficeKey = x.OfficeKey,
                     AmdFacility = x.AmdcodeName,

                 });
            return PartialView("_MappedClinic", mappedClinics);

        }

        [HttpPost]
        public IActionResult SaveClinic(string clinicId, string officeKey, string amdCodeName)
        {
            var existingCp = _urgentData.ClinicProfile.FirstOrDefault(x => x.ClinicId == clinicId);
            _urgentData.ClinicProfile.Attach(existingCp);
            existingCp.OfficeKey = officeKey;
            existingCp.AmdcodeName = amdCodeName;

            _urgentData.SaveChanges();

            return MappedClinic();

        }

        public IActionResult Insurance()
        {

            return View("insurance");
        }

        [HttpGet]
        public IActionResult GetInsurance(int? page, int? limit, string insName)
        {
            var total = 0;
            IQueryable<InsuranceInformation> query;


            if (!string.IsNullOrEmpty(insName))
            {
                query = _urgentData.InsuranceInformation.Where(x => x.PrimaryName.Contains(insName));
            }
            else
            {
                query = _urgentData.InsuranceInformation;

            }
            var records = query.Select(y => new
            {
                y.AmdCode,
                y.InsuranceId,
                y.PrimaryName,
                y.PrimaryPhone,
                y.PrimaryAddress1,
                y.PrimaryAddress2,
                y.PrimaryCity,
                y.PrimaryState,
                y.PrimaryZip
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
        public IActionResult getMapedPh(string officeKey)
        {

            if (!string.IsNullOrEmpty(officeKey))
            {
                var vm = new List<PhysicianVm>();
                vm = _urgentData.Physician.Where(x => x.OfficeKey == officeKey && x.Active).Select(x => new PhysicianVm()
                {
                    AmdDisplayName = x.DisplayName,
                    AmdProfileId = x.AmProviderId,
                    AmdProviderCode = x.AmdCode,
                    pvFirstName = x.FirstName,
                    pvLastName = x.LastName,
                    pvPhysicianId = x.PvPhysicianId,
                }).ToList();
                return PartialView("_MappedPhysician", vm);
            }

            return null;
        }

        [HttpPost]
        public async Task<IActionResult> AddPhysician(string officeKey)
        {
            var vm = new PhysicianVm();
            try
            {

                vm.MappedProviders = vm.MappedProviders = await GetAmdProviderList(officeKey);

                return PartialView("_AddPhysician", vm);
            }

            catch (Exception)
            {
                return PartialView("_Error");
            }

        }


        [HttpPost]
        public async Task<IActionResult> EditPhyisican(int pvPhysicianId, string officeKey)
        {
            var existingPh = _urgentData.Physician.FirstOrDefault(x => x.PvPhysicianId == pvPhysicianId && x.OfficeKey == officeKey);
            var vm = new PhysicianVm
            {
                pvFirstName = existingPh.FirstName,
                pvLastName = existingPh.LastName,
                pvPhysicianId = existingPh.PvPhysicianId
            };
            try
            {
                vm.MappedProviders = await GetAmdProviderList(officeKey);
                vm.MappedProviders.FirstOrDefault(x => x.Value == existingPh.AmProviderId).Selected = true;
                return PartialView("_AddPhysician", vm);
            }

            catch (Exception ex)
            {
                return PartialView("_Error");
            }

        }

        [HttpPost]
        public IActionResult EditClinic(string pvClinicId)
        {

            var existingCp = _urgentData.ClinicProfile.FirstOrDefault(x => x.ClinicId == pvClinicId.Trim());
            var vm = new ClinicVm
            {
                AllClinics = _urgentData.ClinicProfile.Where(x => !string.IsNullOrEmpty(x.ClinicFullName)).DistinctBy(x => x.ClinicId).Select(x => new SelectListItem { Text = x.ClinicId, Value = x.ClinicId }),
                AllOfficeKeys = _urgentData.ClinicProfile.Where(x => !string.IsNullOrEmpty(x.ClinicFullName)).DistinctBy(x => x.OfficeKey).Select(x => new SelectListItem { Text = x.OfficeKey.ToString(), Value = x.OfficeKey.ToString(), Selected = x.OfficeKey == existingCp.OfficeKey }),
                AllFacilities = _urgentData.ClinicProfile.Where(x => !string.IsNullOrEmpty(x.ClinicFullName)).DistinctBy(x => x.AmdcodeName).Select(x => new SelectListItem { Text = x.AmdcodeName, Value = x.AmdcodeName, Selected = x.AmdcodeName == existingCp.AmdcodeName }),
                ClinicId = existingCp.ClinicId
            };
            try
            {
                return PartialView("_EditClinic", vm);
            }

            catch (Exception ex)
            {
                return PartialView("_Error");
            }

        }

        [HttpPost]
        public IActionResult SavePhysician(PhysicianVm physician)
        {
            var pvPhyId = physician.pvPhysicianId.GetValueOrDefault();
            var officeKey = physician.Inputs.OfficeKey;
            if (ModelState.IsValid)
            {
                if (physician.pvPhysicianId.HasValue)
                {


                    if (!_urgentData.Physician.Any(x => x.PvPhysicianId == pvPhyId && x.OfficeKey == officeKey))
                    {
                        _urgentData.Physician.Add(new Physician
                        {
                            PvPhysicianId = physician.pvPhysicianId ?? default(int),
                            AmProviderId = physician.AmdDisplayName,
                            FirstName = physician.pvFirstName,
                            LastName = physician.pvLastName,
                            IsDefault = physician.IsDefault,
                            OfficeKey = physician.Inputs.OfficeKey,
                            Active = true,
                            DisplayName = physician.pvLastName + ", " + physician.pvFirstName

                        });

                    }
                    else
                    {
                        try
                        {
                            _urgentData.Physician.Any(x => x.PvPhysicianId == pvPhyId);

                            var exitingPhy = _urgentData.Physician.FirstOrDefault(x => x.PvPhysicianId == pvPhyId); ;
                            exitingPhy.PvPhysicianId = physician.pvPhysicianId ?? default(int);
                            exitingPhy.AmProviderId = physician.AmdDisplayName;
                            exitingPhy.FirstName = physician.pvFirstName;
                            exitingPhy.LastName = physician.pvLastName;
                            exitingPhy.IsDefault = physician.IsDefault;
                            exitingPhy.DisplayName = physician.pvLastName + ", " + physician.pvFirstName;
                            exitingPhy.Active = true;
                            _urgentData.Attach(exitingPhy);
                        }
                        catch (Exception ex)
                        {
                            return PartialView("_Error");
                        }

                    }
                    _urgentData.SaveChanges();
                }
            }

            return getMapedPh(officeKey);
        }

        [HttpPost]
        public IActionResult DeletePhyisican(int pvPhysicianId, string officeKey)
        {
            var match = _urgentData.Set<Physician>().FirstOrDefault(x => x.PvPhysicianId == pvPhysicianId && x.OfficeKey == officeKey);
            if (match != null)
            {
                _urgentData.Set<Physician>().Remove(match);
                _urgentData.SaveChanges();
            }

            return getMapedPh(officeKey);
        }

        [HttpGet]
        public IActionResult CodeReviewRule()
        {
            var ruleModel = _urgentData.CodeReviewRule.Select(x => new RuleModel
            {
                Id = x.Id,
                RuleName = x.RuleName,
                Enabled = x.Active
            });
            //var ruleModel = null; 
            return View("CodeReviewRule", ruleModel);
        }

        [HttpGet]
        public IActionResult LoadRule(int ruleId)
        {
            var result = _urgentData.CodeReviewRule.First(x => x.Id == ruleId);
            var ruleDetail = new RuleModel
            {
                RuleName = result.RuleName,
                Id = result.Id,
                Definition = !string.IsNullOrEmpty(result.RuleJsonString)
                ? JsonConvert.DeserializeObject<List<RuleItem>>(result.RuleJsonString)
                : new List<RuleItem>()
            };
            return Json(new { success = true, data = ruleDetail });
        }

        [HttpPost]
        public async Task<IActionResult> SaveReviewRule(int ruleId, string ruleName, List<RuleItem> ruleSets)
        {
            var existingRule = _urgentData.CodeReviewRule.FirstOrDefault(x => x.Id == ruleId);
            var sameNameRule = _urgentData.CodeReviewRule.FirstOrDefault(x => x.RuleName == ruleName);

            if (existingRule == null)
            {
                if (sameNameRule == null)
                {
                    _urgentData.CodeReviewRule.Add(new CodeReviewRule
                    {
                        RuleName = ruleName,
                        RuleJsonString = ruleSets.Any() ? JsonConvert.SerializeObject(ruleSets).ToString() : "",
                        CreatedBy = HttpContext.User.Identity.Name,
                        CreatedTime = DateTime.Now
                    });
                }
                else
                {
                    return Json(new { sucess = false, message = "Same rule names alreasy exists." });
                }
            }
            else
            {
                if (sameNameRule == null || (sameNameRule != null && sameNameRule.Id == ruleId))
                {
                    _urgentData.CodeReviewRule.Attach(existingRule);
                    existingRule.RuleName = ruleName;
                    existingRule.LastModifiedBy = HttpContext.User.Identity.Name;
                    existingRule.LastModifiedTime = DateTime.UtcNow;
                    existingRule.RuleJsonString = ruleSets.Any() ? JsonConvert.SerializeObject(ruleSets) : "";
                }
                else
                {
                    return Json(new { sucess = false, message = "Same rule names exisit" });
                }

            }

            try
            {

                await _urgentData.SaveChangesAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "server side error, can not save the rule." });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRule(int ruleId)
        {
            var existingRule = _urgentData.CodeReviewRule.First(x => x.Id == ruleId);

            try
            {
                _urgentData.Remove(existingRule);
                await _urgentData.SaveChangesAsync();
                return Json(new { success = true, last = !_urgentData.CodeReviewRule.Any() });
            }

            catch (Exception ex)
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SetRuleActive(int ruleId, bool isOn)
        {
            var match = _urgentData.CodeReviewRule.First(x => x.Id == ruleId);
            _urgentData.CodeReviewRule.Attach(match);
            match.Active = isOn;
            try
            {
                await _urgentData.SaveChangesAsync();
                return Json(new { success = false });
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }
        }
        private async Task<IList<SelectListItem>> GetAmdProviderList(string officeKey)
        {
            var config = _urgentData.ProgramConfig.FirstOrDefault(x => x.AmdofficeKey == officeKey);
            var apiUri = new Uri(config.Apiuri);

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("apiContext")) || string.IsNullOrEmpty(HttpContext.Session.GetString("redirectUrl")) || string.IsNullOrEmpty(HttpContext.Session.GetString("officekey")) || HttpContext.Session.GetString("officekey") != officeKey)
            {
                var apiResponse = await _apiLoginServicce.ProcessLogin(apiUri, 1, config.ApiuserName, config.Apipassword, config.AmdofficeKey.ToString(), config.AmdAppName, null);
                if (apiResponse.GetType() == typeof(PpmLoginResponse))
                {
                    var sucessResponse = apiResponse;
                    HttpContext.Session.SetString("apiContext", sucessResponse.Results.Usercontext.Text);
                    HttpContext.Session.SetString("redirectUrl", sucessResponse.Results.Usercontext.Webserver + "/xmlrpc/processrequest.asp");
                    HttpContext.Session.SetString("officekey", officeKey);
                }
            }

            var lookupResult = await _lookupService.LookupProviderByName(new Uri(HttpContext.Session.GetString("redirectUrl")), HttpContext.Session.GetString("apiContext"), "");
            return lookupResult.Results.Profilelist.Profile.Where(x => x.Status == "ACTIVE").Select(x => new SelectListItem() { Text = x.Name + " (" + x.Code + ")", Value = x.Id }).ToList();
        }

    }
}