using System;
using System.Collections.Generic;

namespace UgentCareDate.Models
{
    public partial class GuarantorInformation
    {
        public GuarantorInformation()
        {
            GuarantorImportLog = new HashSet<GuarantorImportLog>();
            Visit = new HashSet<Visit>();
        }

        public int PayerNum { get; set; }
        public string RelationshipCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public int PvPatientId { get; set; }

        public ICollection<GuarantorImportLog> GuarantorImportLog { get; set; }
        public ICollection<Visit> Visit { get; set; }
    }
}
