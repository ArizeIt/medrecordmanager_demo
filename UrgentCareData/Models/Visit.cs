using System;
using System.Collections.Generic;

namespace UrgentCareData.Models
{
    public partial class Visit
    {
        public Visit()
        {
            PatientDocument = new HashSet<PatientDocument>();
            PayerInformation = new HashSet<PayerInformation>();
            VisitHistory = new HashSet<VisitHistory>();
            VisitImportLog = new HashSet<VisitImportLog>();
            VisitProcCode = new HashSet<VisitProcCode>();
            VisitICDCode = new HashSet<VisitICDCode>();
            AppliedRules = new HashSet<VisitRuleSet>();
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
        public int PhysicianId { get; set; }
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
        public string OfficeKey { get; set; }
        public int? EmQuantity { get; set; }
        public string FinClass { get; set; }
        public bool Selected { get; set; }

        public virtual Chart Chart { get; set; }
        public virtual ClinicProfile Clinic { get; set; }
        public virtual GuarantorInformation GuarantorPayer { get; set; }
        public virtual Physician Physician { get; set; }
        public virtual PatientInformation PvPatient { get; set; }
        public virtual ICollection<PatientDocument> PatientDocument { get; set; }
        public virtual ICollection<PayerInformation> PayerInformation { get; set; }
        public virtual ICollection<VisitHistory> VisitHistory { get; set; }
        public virtual ICollection<VisitImportLog> VisitImportLog { get; set; }
        public virtual ICollection<VisitProcCode> VisitProcCode { get; set; }
        public virtual ICollection<VisitICDCode> VisitICDCode { get; set; }

        public virtual ICollection<VisitRuleSet> AppliedRules { get; set; }
    }
}
