﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string OfficeKey { get; set; }

        public bool Enabled { get; set; }

        [NotMapped]
        public string ClinicName { get; set; }

        public virtual ICollection<CompanyClinic> CompanyClinics { get; set; }
        public virtual ICollection<UserClinic> UserClinics { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
    }
}
