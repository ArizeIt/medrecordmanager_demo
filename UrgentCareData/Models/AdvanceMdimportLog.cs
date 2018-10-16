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

        public ICollection<ChartImportLog> ChartImportLog { get; set; }
        public ICollection<PatientImportLog> PatientImportLog { get; set; }
        public ICollection<PayerImportLog> PayerImportLog { get; set; }
        public ICollection<VisitImpotLog> VisitImpotLog { get; set; }
    }
}
