using System;
using System.Collections.Generic;
using UrgentCareData.Models;

namespace UrgentCareData.Models
{
    public partial class PayerImportLog
    {
        public int Id { get; set; }
        public int PayerInfoId { get; set; }
        public string Status { get; set; }
        public DateTime ImportedDate { get; set; }
        public int AmdimportId { get; set; }
        public string AmdpayerId { get; set; }
        public string OfficeKey { get; set; }

        public AdvanceMdimportLog Amdimport { get; set; }
    }
}
