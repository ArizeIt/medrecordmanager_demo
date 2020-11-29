﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedRecordManager.Models
{
    public class FilterUser
    {

        [Display(Name = "Clinic")]
        public string Company { get; set; }
        public IEnumerable<SelectListItem> Companies { get; set; }

        [Display(Name = "OfficeKeys")]
        public string OfficeKey { get; set; }

        public IEnumerable<SelectListItem> OfficeKeys { get; set; }

        [Display(Name = "Clinic")]
        public string Clinic { get; set; }
        public IEnumerable<SelectListItem> Clinics { get; set; }

            
    }
}