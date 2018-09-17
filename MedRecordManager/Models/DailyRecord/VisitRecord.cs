namespace MedRecordManager.Models.DailyRecord
{
    public class VisitRecordVm
    {
        public string PatientName { get; set; }

        public int PatientId {get;set;}

        public string ClinicName {get;set;}

        public string PVFinClass {get;set;}
        
        public string DiagCode { get;set;}

        public string OfficeKey{get;set;}

        public string VisitTime{get;set;}

        public string ICDCodes { get; set; }

        public decimal Payment { get; set; }
        public int PvRecordId {get;set;}
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
