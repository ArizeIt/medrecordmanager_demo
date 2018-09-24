using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedRecordManager.Models.DailyRecord
{
    public class DetailRecord
    {
        public ChartDoc VisitChart { get; set; }

        public int ChartId { get; set; }
        public Guarantor GuarantorInfor { get; set; }
    }

    public class Guarantor
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class ChartDoc
    {
        public string FileName { get; set; }

        public int ChartDocId { get; set; }
    }
}   


