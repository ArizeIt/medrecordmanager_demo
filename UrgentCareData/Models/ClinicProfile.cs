using System;
using System.Collections.Generic;

namespace UrgentCareData.Models
{
    public partial class ClinicProfile
    {
        public ClinicProfile()
        {
            CompanyClinics = new HashSet<CompanyClinic>();
            UserClinics = new HashSet<UserClinic>();
            Visits = new HashSet<Visit>();
        }

        public string ClinicFullName { get; set; }
        public string ClinicId { get; set; }
        public string AmdcodeName { get; set; }
        public string AmdcodePrefix { get; set; }
        public int? OfficeKey { get; set; }

        public virtual ICollection<CompanyClinic> CompanyClinics { get; set; }
        public virtual ICollection<UserClinic> UserClinics { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
    }
}
