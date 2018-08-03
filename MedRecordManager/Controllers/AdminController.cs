﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedRecordManager.Models;
using MedRecordManager.Models.PhsycianRecord;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedRecordManager.Controllers
{
    public class AdminController : Controller
    {
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

        public IActionResult getMapedPh(string OfficeKey)
        {
          
            var vm = new List<PhysicianVm>() {

                new PhysicianVm()
            };
            return View("MappedPhysician", vm);
        }

        public IActionResult AddPhysician()
        {
            var vm = new PhysicianVm();
            return View("AddPhysician", vm);
        }
    }
}