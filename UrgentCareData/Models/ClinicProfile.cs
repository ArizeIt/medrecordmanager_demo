using System.Collections.Generic;

namespace UrgentCareData.Models
{
    public partial class ClinicProfile
    {
        public ClinicProfile()
        {
            Visits = new HashSet<Visit>();
        }

        public string ClinicFullName { get; set; }
        public string ClinicId { get; set; }
        public string AmdcodeName { get; set; }
        public string AmdcodePrefix { get; set; }
        public int? OfficeKey { get; set; }

        public ICollection<Visit> Visits { get; set; }
    }
}
