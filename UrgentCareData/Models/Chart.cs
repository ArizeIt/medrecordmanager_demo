using System;
using System.Collections.Generic;

namespace UgentCareDate.Models
{
    public partial class Chart
    {
        public Chart()
        {
            ChartDocument = new HashSet<ChartDocument>();
            Visit = new HashSet<Visit>();
        }

        public DateTime DischargedDate { get; set; }
        public string DischargedBy { get; set; }
        public DateTime SignOffSealedDate { get; set; }
        public string SignedOffSealedBy { get; set; }
        public int ChartId { get; set; }

        public ICollection<ChartDocument> ChartDocument { get; set; }
        public ICollection<Visit> Visit { get; set; }
    }
}
