using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedRecordManager.Models
{
    
    public class RuleModel
    {
        public int Id { get; set; }

        public string RuleName { get; set; }

        public string Description { get; set; }

        public bool Enabled { get; set; }

        public string Definition { get; set; }
    }

  
}
