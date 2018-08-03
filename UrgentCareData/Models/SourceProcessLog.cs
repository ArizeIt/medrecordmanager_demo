using System;

namespace UrgentCareData.Models
{
    public partial class SourceProcessLog
    {
        public int ProcessId { get; set; }
        public string SourceFileName { get; set; }
        public long? FileSize { get; set; }
        public DateTime? ProcessedDate { get; set; }
        public bool? SuccessFlag { get; set; }
        public string ProcessResult { get; set; }
        public bool? MarkAsProcessed { get; set; }
        public bool? MarkDelete { get; set; }
        public bool? ImportedToAmd { get; set; }
    }
}
