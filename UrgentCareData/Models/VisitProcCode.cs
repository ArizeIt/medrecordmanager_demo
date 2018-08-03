using System;
using System.Collections.Generic;
using UrgentCareData.Models;

namespace UgentCareDate.Models
{
    public partial class VisitProcCode
    {
        public string ProcCode { get; set; }
        public int? Quantity { get; set; }
        public int VisitId { get; set; }
        public int VisitProcCodeId { get; set; }

        public Visit Visit { get; set; }
    }
}
