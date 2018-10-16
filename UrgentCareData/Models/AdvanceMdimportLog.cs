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
            VisitImpotLog = new HashSet<VisitImpotLog>();
        }

        public int Id { get; set; }
        public int? SourceProcessId { get; set; }
        public DateTime? ImportedDate { get; set; }
        public string Status { get; set; }

        public virtual ICollection<ChartImportLog> ChartImportLog { get; set; }
        public virtual ICollection<PatientImportLog> PatientImportLog { get; set; }
        public virtual ICollection<PayerImportLog> PayerImportLog { get; set; }
        public virtual ICollection<VisitImpotLog> VisitImpotLog { get; set; }
    }
}
