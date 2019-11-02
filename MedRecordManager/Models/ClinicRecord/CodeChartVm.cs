﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedRecordManager.Models
{
    public class CodeChartVm
    {
        public int VisitId { get; set; }
        public ChartVm Chart { get; set; }

        public int Position { get; set; }

        public int Total { get; set; }
       
    }

    public class ChartVm
    {
       
        public string ChartName { get; set; }

        public byte[] fileBinary { get; set; }

        public bool IsFlaged { get; set; }
    }

       

    public class Code
    {
        public int Id { get; set; }
        public string CodeType { get; set; }
        public string CodeName { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public string ModifierCode { get; set; }
    }

}
