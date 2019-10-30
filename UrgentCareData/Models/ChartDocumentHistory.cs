using System;
using System.Collections.Generic;

namespace UrgentCareData.Models
{
    public partial class ChartDocumentHistory
    {
        public int ChartDocumentHistoryId { get; set; }
        public int VisitHistoryId { get; set; }
        public string UploadedBy { get; set; }
        public DateTime UploadedTime { get; set; }
        public byte[] ChartImage { get; set; }

        public virtual VisitHistory VisitHistory { get; set; }
    }
}
