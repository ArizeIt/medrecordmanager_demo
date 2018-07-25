using System;
using System.Collections.Generic;

namespace UgentCareDate.Models
{
    public partial class ChartImportLog
    {
        public int Id { get; set; }
        public int PvChartDocId { get; set; }
        public string Status { get; set; }
        public DateTime ImportedDate { get; set; }
        public int AmdimportId { get; set; }
        public string AmdFileId { get; set; }

        public AdvanceMdimportLog Amdimport { get; set; }
    }
}
