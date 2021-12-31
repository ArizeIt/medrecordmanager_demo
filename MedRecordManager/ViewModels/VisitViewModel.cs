using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedRecordManager.ViewModels
{
    public class VisitViewModel
    {

        public VisitViewModel()
        {

            this.VisitImportLogs = new List<VisitImportLogViewModel>();

        }
        public System.DateTime ServiceDate { get; set; }
        public int VisitId { get; set; }
        public string ClinicId { get; set; }
        public int PVLogNum { get; set; }
        public string LastUpdateUser { get; set; }
        public System.DateTime LastUpdateTime { get; set; }
        public string DiagCodes { get; set; }
        public string ICDCodes { get; set; }
        public string VisitType { get; set; }
        public string EMCode { get; set; }
        public int PhysicianId { get; set; }
        public Nullable<decimal> CoPayAmount { get; set; }
        public string CopayType { get; set; }
        public string CopayNote { get; set; }
        public Nullable<decimal> PreviousPaymentAmount { get; set; }
        public string PreviousPaymentType { get; set; }
        public string PreviousPaymentNote { get; set; }
        public Nullable<decimal> CurrentPaymentAmount { get; set; }
        public string CurrentPaymentType { get; set; }
        public string CurrentPaymentNote { get; set; }
        public System.DateTime TimeIn { get; set; }
        public System.DateTime TimeOut { get; set; }
        public string Notes { get; set; }
        public int PvPatientId { get; set; }
        public string ProcCodes { get; set; }
        public Nullable<int> ProcQty { get; set; }
        public Nullable<int> ChartId { get; set; }
        public int GuarantorPayerId { get; set; }
        public Nullable<int> SourceProcessId { get; set; }
        public bool Flagged { get; set; }
        public bool IsModified { get; set; }
        public string OfficeKey { get; set; }

        public string FinClass { get; set; }

        public string ImportStatus { get; set; }

        public DateTime? ImportStatusDate { get; set; }

        public  ICollection<VisitImportLogViewModel> VisitImportLogs { get; set; }

        public  PatientInformationViewModel PatientInformation { get; set; }
    }
}