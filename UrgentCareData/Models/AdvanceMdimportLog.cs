using System;
using System.Collections.Generic;

namespace UrgentCareData.Models
{
    public partial class AdvanceMdimportLog
    {
        public AdvanceMdimportLog()
        {
            ChartImportLog = new HashSet<ChartImportLog>();
            PatientImportLog = new HashSet<PatientImportLog>();
            PayerImportLog = new HashSet<PayerImportLog>();
            VisitImportLog = new HashSet<VisitImportLog>();
        }

        public int Id { get; set; }
        public int? SourceProcessId { get; set; }
        public DateTime? ImportedDate { get; set; }
        public string Status { get; set; }

        public virtual ICollection<ChartImportLog> ChartImportLog { get; set; }
        public virtual ICollection<PatientImportLog> PatientImportLog { get; set; }
        public virtual ICollection<PayerImportLog> PayerImportLog { get; set; }
        public virtual ICollection<VisitImportLog> VisitImportLog { get; set; }
    }
}
