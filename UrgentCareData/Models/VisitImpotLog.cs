using System;
using System.Collections.Generic;

namespace UrgentCareData.Models
{
    public partial class VisitImpotLog
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string OfficeKey { get; set; }
        public DateTime ImportedDate { get; set; }
        public int AmdimportLogId { get; set; }
        public int VisitId { get; set; }
        public string AmdvisitId { get; set; }
        public bool? ChargeImported { get; set; }

        public AdvanceMdimportLog AmdimportLog { get; set; }
        public Visit Visit { get; set; }
    }
}
