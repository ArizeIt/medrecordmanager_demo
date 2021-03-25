using MedRecordManager.Areas.Identity.Pages.Account;
using MedRecordManager.Data;
using MedRecordManager.Extension;
using MedRecordManager.Models;
using MedRecordManager.Models.UserRecord;
using MedRecordManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrgentCareData;
using UrgentCareData.Models;

namespace MedRecordManager.Controllers
{
    [Authorize]
    public class UserAdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;        
        private readonly UrgentCareContext _urgentCareContext;
        private readonly AppAdminContext _appAdminContext;
        private readonly UserManager<ApplicationUser> _userManager;
       
        public UserAdminController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, UrgentCareContext urgentCareContext, AppAdminContext appAdminContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _urgentCareContext = urgentCareContext;
            _appAdminContext = appAdminContext;
        }


        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            if (roleName != null)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName.Trim()));
            }
            return RedirectToAction("ManageRole");
        }

        public IActionResult ManageUser()
        {
            var userId = _userManager.GetUserId(User);
            var vm = new UserVm();
            if(User.IsInRole("SuperAdmin"))
            {
                vm.AvaliableComps = _appAdminContext.CompanyProfile.Select(x => new SelectListItem
                {
                    Text = x.CompanyName,
                    Value = x.Id.ToString()
                });

                vm.Filter = new FilterUser
                {
                    Clinic = string.Empty,
                    OfficeKey = string.Empty,
                    Company = string.Empty,
                };              
            }

          
            
            return View("ManageUser", vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewUser(UserVm user)
        {
            if (ModelState.IsValid)
            {
                var appUser = new ApplicationUser
                {
                    UserName = user.Email,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                };

                IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);
                if (User.IsInRole("SuperAdmin") && !result.Errors.Any())
                {
                    _appAdminContext.UserCompany.Add(new UserCompany
                    {
                        CompanyId = Int32.Parse(user.Company),
                        UserId = appUser.Id,
                        UserName = appUser.UserName
                    });
                }
                else
                {

                }

                _urgentCareContext.SaveChanges();
                if (result.Succeeded)
                {
                    RedirectToAction("ManageUser");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                user.HasError = true;

            }

            user.Filter = new FilterUser
            {
                Clinic = string.Empty,
                OfficeKey = string.Empty,
                Company = string.Empty,
            };
            return View("ManageUser", user);
        }

        public IActionResult LoadAddUser()
        {
            var model = new RegisterModel.InputModel();
            return PartialView("_AddUserView", model);
        }

        public async Task<IActionResult> GetUsers(int? page, int? limit)
        {
            var userId = _userManager.GetUserId(User);
            var users = _userManager.Users.ToList();
            var records = new List<UserVm>();
            if (User.IsInRole("CompanyAdmin"))
            {
                var companyIds = await _appAdminContext.UserCompany.Where(x => x.UserId == userId).Select(x=> x.CompanyId).ToListAsync();
                var companyUsers = await _appAdminContext.UserCompany.Where(x => companyIds.Contains(x.CompanyId)).Select(x => x.UserId).ToListAsync();
                users = users.Where(x => companyUsers.Contains(x.Id)).ToList();

                foreach (var user in users)
                {
                    var thisUser = new UserVm
                    {
                        UserId = user.Id,
                        FirstName = user.FirstName,
                        Email = user.Email,
                        LastName = user.LastName
                    };

                    var roles = await _userManager.GetRolesAsync(user);
                    thisUser.Roles = string.Join("</br>", roles);


                    var companies = await _appAdminContext.CompanyProfile.Where(x => companyIds.Contains(x.Id)).Select(x => x.DisplayName).ToListAsync();
                    thisUser.Company = string.Join("</br>", companies);

                    var offices = await _appAdminContext.UserOfficeKey.Where(x => x.UserId == user.Id).Select(x => x.OfficeKey).ToListAsync();
                    thisUser.OfficeKeys = string.Join("</br>", offices);

                    var clinics = await _appAdminContext.UserClinic.Where(x => x.UserId == user.Id).ToListAsync();
                    thisUser.Clinics = string.Join("</br>", clinics);

                    records.Add(thisUser);
                }
            }
    

            var total = records.Count();

            if (page.HasValue && limit.HasValue)
            {
                var start = (page.Value - 1) * limit.Value;
                records = records.Skip(start).Take(limit.Value).ToList();


            }
            return Json(new { records, total });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(string userName)
        {
            var currentuser = this.User;

            var user = await _userManager.FindByIdAsync(userName);
            if (user != null)
            {
                if (currentuser.Identity.Name != user.Email)
                {
                    var result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        return Json(new { success = true });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "You can not delete yourself while you are logged in. " });
                }
            }

            return Json(new { success = false, message = "Either this user does not exisits or there is some thing wrong when tried to delete." });
        }
        public async Task<IActionResult> ManageRole()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View("ManageRole", roles);
        }

        public IActionResult ManageCompany()
        {
            return View("ManageCompany");
        }
        public IActionResult ManageClinic()
        {

            return View("ManageClinic");
        }

        public IActionResult ManageOffice()
        {

            return View("ManageOffice");
        }

        public async Task<IActionResult> GetUserCompanies(int? page, int? limit)
        {
            var records = new List<CompanyProfile>();
            if (User.IsInRole("SuperAdmin"))
            {
                records = await _appAdminContext.CompanyProfile.ToListAsync();
            }
            else if(User.IsInRole("CompanyAdmin"))
            {
                var companies = await _appAdminContext.UserCompany.Where(x => x.UserName == User.Identity.Name).Select(x => x.CompanyId).ToListAsync();
                records = await _appAdminContext.CompanyProfile.Where(x => companies.Contains(x.Id)).ToListAsync();
            }
            

           
            var total = records.Count();

            if (page.HasValue && limit.HasValue)
            {
                var start = (page.Value - 1) * limit.Value;
                records = records.Skip(start).Take(limit.Value).ToList();


            }
            return Json(new { records, total });
        }

        public async Task<IActionResult> getUserClinics(int? page, int? limit)
        {
            var records = new List<ClinicProfile>();
            if (User.IsInRole("SuperAdmin"))
            {
                records = await _urgentCareContext.ClinicProfile.Where(x => x.OfficeKey != null).ToListAsync();
            }
            else if (User.IsInRole("CompanyAdmin"))
            {
                var companyIds = await _appAdminContext.UserCompany.Where(x => x.UserName == User.Identity.Name).Select(x => x.CompanyId).ToListAsync();
                var clincIds = await _urgentCareContext.CompanyClinic.Where(x => companyIds.Contains(x.CompanyId)).Select(x => x.ClinicId).ToListAsync();
                records = await _urgentCareContext.ClinicProfile.Where(x=> clincIds.Contains(x.ClinicId)).ToListAsync();
            }
            records.ForEach(x => x.ClinicName = x.ClinicId);
            var total = records.Count();

            if (page.HasValue && limit.HasValue)
            {
                var start = (page.Value - 1) * limit.Value;
                records = records.Skip(start).Take(limit.Value).ToList();


            }
            return Json(new { records, total });
        }

        public async Task<IActionResult> getUserOffices(int? page, int? limit)
        {

            var records = await _urgentCareContext.ProgramConfig.Where(x => x.Enabled).Select(x => new { officeKey = x.AmdofficeKey }).ToListAsync();

            var total = records.Count();

            if (page.HasValue && limit.HasValue)
            {
                var start = (page.Value - 1) * limit.Value;
                records = records.Skip(start).Take(limit.Value).ToList();


            }
            return Json(new { records, total });
        }

        [HttpGet]
        public async Task<IActionResult> AssignPermission(string userId)
        {
            var currentUser = new UserVm();
            var user = await _userManager.FindByIdAsync(userId);
            var aRoles = await _userManager.GetRolesAsync(user);
            var sRoles = await _roleManager.Roles.ToListAsync();
            

            if (user != null)
            {

                currentUser.Email = user.Email;
                currentUser.FirstName = user.FirstName;
                currentUser.LastName = user.LastName;
                currentUser.UserId = user.Id;

            }

            var isSuperAdmin = await _userManager.IsInRoleAsync(user, "SuperAdmin");
            if (!isSuperAdmin)
            {
                currentUser.AvaliableRoles = await _roleManager.Roles.Where(x => !aRoles.Contains(x.Name)).Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id

                }).ToListAsync();
            }

            var roleNames = await _userManager.GetRolesAsync(user);
            currentUser.AssignedRoles = await _roleManager.Roles.Where(x => roleNames.Contains(x.Name)).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id

            }).ToListAsync();

            currentUser.AvaliableComps = await _appAdminContext.CompanyProfile.Select(x => new SelectListItem
            {
                Text = x.CompanyName,
                Value = x.Id.ToString()
            }).ToListAsync();

            var companyIds = await _appAdminContext.UserCompany.Where(x => x.UserId == currentUser.UserId).Select(x => x.CompanyId).ToListAsync();
            currentUser.AssignedComps = await _appAdminContext.UserCompany.Where(x => companyIds.Contains(x.CompanyId)).Select(x => new SelectListItem
            {
                Text = x.Company.CompanyName,
                Value = x.CompanyId.ToString()

            }).ToListAsync();

            currentUser.AvaliableOffices = _urgentCareContext.ClinicProfile.Where(x => x.OfficeKey != null).DistinctBy(x => x.OfficeKey).Select(x => new SelectListItem
            {
                Text = x.OfficeKey.ToString(),
                Value = x.OfficeKey.ToString()
            }).ToList();

            currentUser.AssignedOffices = await _appAdminContext.UserOfficeKey.Where(x => x.UserId == currentUser.UserId).Select(x => new SelectListItem
            {
                Text = x.OfficeKey.ToString(),
                Value = x.OfficeKey.ToString()

            }).ToListAsync();

            currentUser.AvailableClinics = _urgentCareContext.ClinicProfile.DistinctBy(x => x.ClinicId).Select(x => new SelectListItem
            {
                Text = x.ClinicId,
                Value = x.ClinicId
            }).ToList();

            currentUser.AssignedClinics = await _appAdminContext.UserClinic.Where(x => x.UserId == currentUser.UserId).Select(x => new SelectListItem
            {
                Text = x.ClinicId,
                Value = x.ClinicId

            }).ToListAsync();

            return PartialView("~/Views/UserAdmin/UserPermission.cshtml", currentUser);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserPerm(string userId, string firstName, string lastName, List<string> assignedRoles, List<string> assignedComps, List<string> assignedOffices, List<string> assignedClinics)
        {
            try
            {
                var appUser = await _userManager.FindByIdAsync(userId);
                var currentRoles = await _userManager.GetRolesAsync(appUser);

                await _userManager.RemoveFromRolesAsync(appUser, currentRoles);
                await _userManager.AddToRolesAsync(appUser, assignedRoles);

                var oldComps = await _appAdminContext.UserCompany.Where(x => x.UserId == userId).ToListAsync();
                _appAdminContext.UserCompany.RemoveRange(oldComps);
                foreach (var comp in assignedComps)
                {
                    _appAdminContext.UserCompany.Add(new UserCompany
                    {
                        UserId = userId,
                        CompanyId = int.Parse(comp),
                        UserName = appUser.UserName
                    });
                }

                var oldOffices = await _appAdminContext.UserOfficeKey.Where(x => x.UserId == userId).ToListAsync();
                _appAdminContext.UserOfficeKey.RemoveRange(oldOffices);
                foreach (var office in assignedOffices)
                {
                    _appAdminContext.UserOfficeKey.Add(new UserOfficeKey
                    {
                        UserId = userId,
                        OfficeKey = int.Parse(office)
                    });
                }

                var oldClinics = await _appAdminContext.UserClinic.Where(x => x.UserId == userId).ToListAsync();
                _appAdminContext.UserClinic.RemoveRange(oldClinics);
                foreach (var clinic in assignedClinics)
                {
                    _appAdminContext.UserClinic.Add(new UserClinic
                    {
                        UserId = userId,
                        ClinicId = clinic
                    });
                }

                _urgentCareContext.SaveChanges();
            }
            catch
            {
                return Json(new { success = false });
            }

            return RedirectToAction("AssignPermission", new { userId });
        }
        //public async Task<IActionResult> getAvaliableRoles()
        //{
        //    var roles = new List<SelectListItem>();
        //    var user = await _userManager.GetUserAsync(User);
        //    var assinedroles =await _userManager.GetRolesAsync(user);
        //    if(User.IsInRole("SuperAdmin"))
        //    {
        //        roles=  await _roleManager.Roles.Select(x=> new SelectListItem { 
        //            Text = x.Name,
        //            Value = x.Id

        //        }).ToListAsync();
        //    }

        //return Json(roles);
        //}
    }
}