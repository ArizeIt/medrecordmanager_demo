using System;
using System.Collections.Generic;

namespace UrgentCareData.Models
{
    public partial class CodeReviewRule
    {
        public string RuleName { get; set; }
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string RuleJsonString { get; set; }

        public bool Active { get; set; }
    }
}
