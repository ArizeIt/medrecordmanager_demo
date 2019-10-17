using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedRecordManager.Models
{
    public class CodeChartVm
    {
        public int VisitId { get; set; }
        public ChartVm Chart { get; set; }
       
    }

    public class ChartVm
    {
       
        public string ChartName { get; set; }

        public byte[] fileBinary { get; set; }
    }

    public class IcdCode
    {
        public string Code { get; set; }
        public string Description { get; set; }

    }

    public class ProcCode
    {
        public string CodeName { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }
    }
}
