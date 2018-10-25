using System;
using System.Collections.Generic;
using System.Text;

namespace UrgentCareData.Models
{
    public class Audit
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public DateTime ModifiedTime { get; set; }
        public string KeyValues { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }

        public string ModifiedBy { get; set; }
    }
}
