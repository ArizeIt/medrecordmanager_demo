using System;

namespace UrgentCareData.Models
{
    public partial class GuarantorImportLog
    {
        public int Id { get; set; }
        public int PayerNumber { get; set; }
        public string Status { get; set; }
        public DateTime ImportedDate { get; set; }
        public int AmdimportId { get; set; }
        public string AmdResponsiblePartyId { get; set; }
        public string OfficeKey { get; set; }

        public virtual GuarantorInformation PayerNumberNavigation { get; set; }
    }
}
