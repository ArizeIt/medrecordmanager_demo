using System;
using System.Collections.Generic;

namespace UrgentCareData.Models
{
    public partial class VisitCodeHistory
    {
        public string CodeType { get; set; }
        public string Code { get; set; }
        public int? Quantity { get; set; }
        public string Modifier { get; set; }
        public string Modifier2 { get; set; }
        public int VisitHistoryId { get; set; }
        public int VisitCodeHistoryId { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedTime { get; set; }
        public string Action { get; set; }
        public virtual VisitHistory VisitHistory { get; set; }
    }
}
