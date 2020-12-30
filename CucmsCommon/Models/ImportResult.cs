using System;

namespace CucmsCommon.Models
{
    public class ImportResult
    {
        public DateTime JobstartedTime { get; set; }

        public DateTime JobEndTime { get; set; }

        public string SourceFileName { get; set; }

        public int TotalRecords { get; set; }

        public int ProcessedRecord { get; set; }
    }
}
