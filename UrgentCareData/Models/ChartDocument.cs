using System;
using System.Collections.Generic;

namespace UrgentCareData.Models
{
    public partial class ChartDocument
    {
        public string FileName { get; set; }
        public short FileType { get; set; }
        public short NumberOfPages { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public int PatNum { get; set; }
        public int ChartDocId { get; set; }
        public int ChartId { get; set; }
        public byte[] DocumentImage { get; set; }

        public virtual Chart Chart { get; set; }
    }
}
