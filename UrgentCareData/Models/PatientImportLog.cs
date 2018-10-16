using System;
using System.Collections.Generic;
using UrgentCareData.Models;

namespace UrgentCareData.Models
{
    public partial class PatientImportLog
    {
        public int Id { get; set; }
        public int PvpatientId { get; set; }
        public string AmdpatientId { get; set; }
        public int AmdimportId { get; set; }
        public string Status { get; set; }
        public DateTime ImportedDate { get; set; }
        public string OfficeKey { get; set; }

        public AdvanceMdimportLog Amdimport { get; set; }
    }
}
