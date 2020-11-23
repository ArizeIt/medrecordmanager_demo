using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedRecordManager.Data;
using MedRecordManager.Extension;
using MedRecordManager.Models;
using MedRecordManager.Models.UserRecord;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            if (roleName != null)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName.Trim()));
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ManageUser()
        {
            var users = await _userManager.Users.ToListAsync();
           var  userId = _userManager.GetUserId(User);
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

    }
}