using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UrgentCareData.Models
{
    public partial class BulkVisit
    {
        public BulkVisit()
        {
          
            VisitICDCodes = new HashSet<BulkVisitICDCode>();
            VisitProcCodes = new HashSet<BulkVisitProcCode>();
        }
        public DateTime ServiceDate { get; set; }
        public int VisitId { get; set; }
        public string ClinicId { get; set; }
        public int PvlogNum { get; set; }
        public string LastUpdateUser { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public string DiagCodes { get; set; }
        public string Icdcodes { get; set; }
        public string VisitType { get; set; }
        public string Emcode { get; set; }
        public string EmModifier { get; set; }
        public int PhysicanId { get; set; }
        public decimal? CoPayAmount { get; set; }
        public string CopayType { get; set; }
        public string CopayNote { get; set; }
        public decimal? PreviousPaymentAmount { get; set; }
        public string PreviousPaymentType { get; set; }
        public string PreviousPaymentNote { get; set; }
        public decimal? CurrentPaymentAmount { get; set; }
        public string CurrentPaymentType { get; set; }
        public string CurrentPaymentNote { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
        public string Notes { get; set; }
        public int PvPatientId { get; set; }
        public string ProcCodes { get; set; }
        public int? ProcQty { get; set; }
        public int? ChartId { get; set; }
        public int GuarantorPayerId { get; set; }
        public int? SourceProcessId { get; set; }
        public bool Flagged { get; set; }
        public bool IsModified { get; set; }
        public int? OfficeKey { get; set; }
        public int? EmQuantity { get; set; }
        public string PatientName { get; set; }
        public string FinClass { get; set; }
        public bool Selected { get; set; }
        [NotMapped]
        public string PhysicanName { get; set; }
        public bool UnFlagged { get; set; }
        public virtual ICollection<BulkVisitICDCode> VisitICDCodes { get; set; }
        public virtual ICollection<BulkVisitProcCode> VisitProcCodes { get; set; }

    }
}

