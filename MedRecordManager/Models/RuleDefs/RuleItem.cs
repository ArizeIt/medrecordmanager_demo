using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedRecordManager.Models
{
    public class CodeReviewRule
    {
        public IEnumerable<RuleItem> RuleList { get; set; }

        public RuleDefination RuleQuery { get; set; }
    }

    public class RuleItem
    {
        public int Id { get; set; }

        public string RuleName { get; set; }

        public string Description { get; set; }

        public bool Enabled { get; set; }

        public string Defination { get; set; }
    }

    public class RuleDefination
    {

    }
}
