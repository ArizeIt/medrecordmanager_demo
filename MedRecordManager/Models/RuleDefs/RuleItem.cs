using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedRecordManager.Models.RuleDefs
{
    public class RuleItem
    {
        public int Id { get; set; }

        public string RuleName { get; set; }

        public string Description { get; set; }

        public bool Enabled { get; set; }

        public string Defination { get; set; }
    }
}
