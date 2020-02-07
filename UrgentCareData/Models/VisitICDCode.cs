using System;
using System.Collections.Generic;
using System.Text;

namespace UrgentCareData.Models
{
    public class VisitICDCode
    {
        public string ICDCode { get; set; }

        public int VisitId { get; set; }
        public int VisitICDCodeId { get; set; }
        public virtual Visit Visit { get; set; }
    }
}
