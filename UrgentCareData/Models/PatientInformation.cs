using System;
using System.Collections.Generic;

namespace UrgentCareData.Models
{
    public partial class PatientInformation
    {
        public PatientInformation()
        {
            Visit = new HashSet<Visit>();
        }

        public int PatNum { get; set; }
        public string Ssn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Birthday { get; set; }
        public string Sex { get; set; }
        public string CellPhone { get; set; }
        public string HomePhone { get; set; }
        public string Email { get; set; }

        public ICollection<Visit> Visit { get; set; }
    }
}
