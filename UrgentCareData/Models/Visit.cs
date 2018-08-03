using System;
using System.Collections.Generic;
using UgentCareDate.Models;

namespace UrgentCareData.Models
{
    public partial class Visit
    {
        public Visit()
        {
            PayerInformation = new HashSet<PayerInformation>();
            VisitProcCode = new HashSet<VisitProcCode>();
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
        public int PvPaitentId { get; set; }
        public string ProcCodes { get; set; }
        public int? ProcQty { get; set; }
        public int? ChartId { get; set; }
        public int GuarantorPayerId { get; set; }
        public int? SourceProcessId { get; set; }

        public Chart Chart { get; set; }
        public GuarantorInformation GuarantorPayer { get; set; }
        public Physican Physican { get; set; }
        public PatientInformation PvPaitent { get; set; }

        public ClinicProfile ClinicProfile { get; set; }
        public ICollection<PayerInformation> PayerInformation { get; set; }
        public ICollection<VisitProcCode> VisitProcCode { get; set; }
    }
}
