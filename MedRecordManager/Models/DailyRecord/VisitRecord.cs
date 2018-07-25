using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedRecordManager.Models
{
    public class VisitRecordVm
    {
        public string PatientName { get; set; }

        public int PatientId {get;set;}

        public string ClinicName {get;set;}

        public string PVFinClass {get;set;}
        
        public string DiagCode { get;set;}

        public string OfficeKey{get;set;}

        public DateTime VisitTime{get;set;}

        public int PvRecordId {get;set;}
    }

    public class DataTableVm
    {
        public IEnumerable<VisitRecordVm> Data { get; set; }

        public int RecordTotal { get; set; }

    }
}
