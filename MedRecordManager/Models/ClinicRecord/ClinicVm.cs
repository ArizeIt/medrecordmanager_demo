using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MedRecordManager.Models
{
    public class ClinicVm
    {

        public string ClinicId { get; set; }

        public string OfficeKey { get; set; }

        public string AmdFacility { get; set; }


        public IEnumerable<SelectListItem> AllClinics { get; set; }
        public IEnumerable<SelectListItem> AllOfficeKeys { get; set; }
        public IEnumerable<SelectListItem> AllFacilities { get; set; }

    }
}
