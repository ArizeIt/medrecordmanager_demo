using System;
using System.Collections.Generic;
using System.Text;

namespace UrgentCareData.Models
{
    public class BulkVisitICDCode
    {
        public string ICDCode { get; set; }

        public int VisitId { get; set; }
        public int BulkVisitICDCodeId { get; set; }
        public virtual BulkVisit BulkVisit { get; set; }
    }
}
