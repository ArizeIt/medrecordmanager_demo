using System;

namespace UrgentCareData.Models
{
    public partial class ChartDocumentHistory
    {
        public int ChartDocumentHistoryId { get; set; }
        public int VisitHistoryId { get; set; }
        public string UploadedBy { get; set; }
        public DateTime UploadedTime { get; set; }
        public byte[] ChartImage { get; set; }
        public string Filename { get; set; }
        public bool IsTemp { get; set; }
        public virtual VisitHistory VisitHistory { get; set; }
    }
}
