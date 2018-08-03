using System.Collections.Generic;
using UgentCareDate.Models;

namespace UrgentCareData.Models
{
    public partial class InsuranceInformation
    {
        public InsuranceInformation()
        {
            PayerInformation = new HashSet<PayerInformation>();
        }

        public string PrimaryName { get; set; }
        public string PrimaryPhone { get; set; }
        public string PrimaryAddress1 { get; set; }
        public string PrimaryAddress2 { get; set; }
        public string PrimaryCity { get; set; }
        public string PrimaryState { get; set; }
        public string PrimaryZip { get; set; }
        public int InsuranceId { get; set; }
        public string AmdCode { get; set; }

        public ICollection<PayerInformation> PayerInformation { get; set; }
    }
}
