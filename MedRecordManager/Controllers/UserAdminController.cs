using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedRecordManager.Areas.Identity.Pages.Account;
using MedRecordManager.Data;
using MedRecordManager.Extension;
using MedRecordManager.Models;
using MedRecordManager.Models.UserRecord;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrgentCareData;

namespace MedRecordManager.Controllers
{

    public class UserAdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _userContext;
        private readonly UrgentCareContext _urgentCareContext;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserAdminController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, UrgentCareContext _urgentCareContext, ApplicationDbContext _userContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            this._urgentCareContext = _urgentCareContext;
            this._userContext = _userContext;
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
            var vm = new FilterUser
            {
                //Clinics = users.DistinctBy(x => x.ClinicId).Select(y => new SelectListItem
                //{
                //    Selected = false,
                //    Text = y.ClinicId,
                //    Value = y.ClinicId,
                //}).OrderBy(r => r.Text),
                //Physicians = users.DistinctBy(x => x.PhysicanId).Select(y => new SelectListItem
                //{
                //    Selected = false,
                //    Text = y.Physican.DisplayName,
                //    Value = y.PhysicanId.ToString(),
                //}).OrderBy(r => r.Text),
                //FinClasses = records.DistinctBy(x => x.PayerInformation.Select(y => y.Class)).Select(y => new SelectListItem
                //{
                //    Selected = false,
                //    Text = y.PayerInformation.FirstOrDefault() != null ? y.PayerInformation.FirstOrDefault().Class.ToString() : "None",
                //    Value = y.PayerInformation.FirstOrDefault() != null ? y.PayerInformation.FirstOrDefault().Class.ToString() : "None"
                //}).DistinctBy(z => z.Text).OrderByDescending(r => r.Text),

                Clinic = string.Empty,
                OfficeKey = string.Empty,
                Company = string.Empty,
            };
            return View("ManageUser", vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewUser(string email, string firstName, string lastName)
        {
            if (!string.IsNullOrEmpty(email)) {
                var appUser = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName
                };

                IdentityResult result = await _userManager.CreateAsync(appUser);
                if (result.Succeeded)
                    return Json(new { success = true, responseText = "The user  was added." });
                else
                {
                    return Json(new { success = false, responseText = string.Join("</br>", result.Errors) });
                }

            }

            return Json(new { success = false, responseText = "The user  was not added." });
        }

        public IActionResult LoadAddUser()
        {
            var model = new RegisterModel.InputModel();
            return PartialView("_AddUserView", model);
        }

        public async Task<IActionResult> GetUsers(int? page, int? limit)
        {
            var users = _userManager.Users.ToList();
            var records = new List<UserVm>();
            foreach (var user in users)
            {
                var thisUser = new UserVm
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    Email = user.Email
                };

                var roles = await _userManager.GetRolesAsync(user);
                thisUser.Roles = string.Join("</br>", roles);
                records.Add(thisUser);
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
            var user = await _userManager.FindByIdAsync(userName);
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Json(new { success = true});
            }
            return Json(new { success = false });
        }

        public async Task<IActionResult> ManageRole()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View("ManageRole",roles);
        }

       
       
    }
}