using System;
using System.Collections.Generic;

namespace UrgentCareData.Models
{
    public partial class Physican
    {
        public Physican()
        {
            Visit = new HashSet<Visit>();
        }

        public int PvPhysicanId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Clinic { get; set; }
        public string AmProviderId { get; set; }
        public string DisplayName { get; set; }
        public int OfficeKey { get; set; }
        public string AmdCode { get; set; }
        public bool IsDefault { get; set; }

        public virtual ICollection<Visit> Visit { get; set; }
    }
}
