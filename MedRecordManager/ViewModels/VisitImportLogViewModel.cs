using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedRecordManager.ViewModels
{
    public  class VisitImportLogViewModel
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string OfficeKey { get; set; }
        public System.DateTime ImportedDate { get; set; }
        public int AMDImportLogId { get; set; }
        public int VisitId { get; set; }
        public string AMDVisitId { get; set; }
        public Nullable<bool> ChargeImported { get; set; }
    }
}
