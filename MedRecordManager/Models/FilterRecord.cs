using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MedRecordManager.Models
{
    public class FilterRecord
    {
        [Display(Name = "Clinic")]
        public string Clinic { get; set; }
        public IEnumerable<SelectListItem> Clinics { get; set; }

        [Display(Name = "Physician")]
        public string Physician { get; set; }

        public IEnumerable<SelectListItem> Physicians { get; set; }

        [Display(Name = "Fin Class")]
        public string FinClass { get; set; }
        public IEnumerable<SelectListItem> FinClasses { get; set; }

        public string FlaggedRule { get; set; }

        [Display(Name = "Flagged Rule")]
        public IEnumerable<SelectListItem> FlaggedRules { get; set; }

        [DisplayName("Start Date Time:"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime StartDate { get; set; }

        [DisplayName("End Date Time:"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime EndDate { get; set; }

        public IList<int> AppliedRuleIds { get; set; }

        public bool AllSelected { get; set; }

    }
}
