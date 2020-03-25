using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedRecordManager.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class UserAdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserAdminController(RoleManager<IdentityRole> roleManager )
        {
            _roleManager = roleManager;
        }
        
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View("CreateRole");
        }

    }
}