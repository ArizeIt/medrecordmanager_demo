using System;
using System.Collections.Generic;
using System.Text;

namespace UrgentCareData.Models
{
    public partial class VisitRuleSet
    {
        public int RuleSetId { get; set; }
        public int VisitId { get; set; }
        public int VisitRuleId { get; set; }
        public virtual CodeReviewRule CodeReviewRuleSet{get;set;}   
        public virtual Visit Visit { get; set; }
    }
}
