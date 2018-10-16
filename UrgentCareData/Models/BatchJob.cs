using System;
using System.Collections.Generic;

namespace UrgentCareData.Models
{
    public partial class BatchJob
    {
        public int BatchJobId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? FinishedDateTime { get; set; }
        public string CreatedBy { get; set; }
        public string JobStatus { get; set; }
        public int TotalRecords { get; set; }
        public long RecordsProcessed { get; set; }
        public int RecordsSucceed { get; set; }
        public string JobName { get; set; }
        public string SessionId { get; set; }
        public string Paramters { get; set; }
    }
}
