using System;
using System.Collections.Generic;



namespace UrgentCareData.Models
{
    public partial class CompanyClinic
    {
        public string ClinicId { get; set; }
        public int CompanyId { get; set; }
        public int Id { get; set; }

        public virtual ClinicProfile Clinic { get; set; }
        public virtual CompanyProfile Company { get; set; }
    }
}
