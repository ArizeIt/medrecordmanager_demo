using System;
using System.Collections.Generic;

namespace UrgentCareData.Models
{
    public partial class VisitHistory
    {
        public VisitHistory()
        {
            ChartDocumentHistory = new HashSet<ChartDocumentHistory>();
            VisitCodeHistory = new HashSet<VisitCodeHistory>();
        }

        public DateTime ServiceDate { get; set; }
        public int VisitHistoryId { get; set; }
        public int VisitId { get; set; }
        public string DiagCodes { get; set; }
        public string Icdcodes { get; set; }
        public string Emcode { get; set; }
        public string CopayNote { get; set; }
        public string ProcCodes { get; set; }
        public int? ProcQty { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedTime { get; set; }
        public DateTime? FinalizedTime { get; set; }
        public bool Saved { get; set; }

        public virtual Visit Visit { get; set; }
        public virtual ICollection<ChartDocumentHistory> ChartDocumentHistory { get; set; }
        public virtual ICollection<VisitCodeHistory> VisitCodeHistory { get; set; }
    }
}
