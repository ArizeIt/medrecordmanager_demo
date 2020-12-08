using System;
using System.Collections.Generic;

namespace MedRecordManager.Models.DailyRecord
{
    public class VisitRecordVm
    {
        public string PatientName { get; set; }

        public int PatientId {get;set;}

        public string ClinicName {get;set;}

        public string PVFinClass {get;set;}
        
        public string DiagCode { get;set;}

        public int OfficeKey{get;set;}

        public string VisitTime{get;set;}

        public string IcdCodes { get; set; }

        public decimal Payment { get; set; }

        public int PvRecordId {get; set;}

        public string ProcCodes { get; set; }

        public int VisitId { get; set; }

        public bool IsFlagged { get; set; }

        public int PhysicanId { get; set; }

        public string PhysicianName { get; set; }

        public string InsuranceName { get; set; }

        public string ImportedDate { get; set; }

        public string ChargeImported { get; set; }

        public string PatChartImported { get; set; }

        public string PatDocImported { get; set; }

        public string AppliedRules { get; set; }

        public DateTime ServiceDate { get; set; }

        public bool Selected { get; set; }

    }

    public class PatientVisitVM
    {
        public string PatientName { get; set; }

        public string PvClinic { get; set; }

        public string VisitDate { get; set; }

        public string PvId { get; set; }

        public string PvPhone { get; set; }

        public string PvPhoneType { get; set; }

        public string CellPhone { get; set; }
    }
}
