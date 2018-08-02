using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MedRecordManager.Models
{
    public class SearchInputs
    {
        [Display(Name = "Office Key")]
        public int OfficeKey { get; set; }
        public IEnumerable<SelectListItem> OfficeKeys { get; set; }

        [Display(Name = "Clinic Name")]
        public string Clinic { get; set; }
        public IEnumerable<SelectListItem> Clinics { get; set; }

        [DisplayName("Start Date Time:"), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss tt}")]
        public DateTime StartDate{get;set;}

        [DisplayName("End Date Time:"), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss tt}")]
        public DateTime EndDate{get;set;}


        public string Type { get; set; }
    }

    
}