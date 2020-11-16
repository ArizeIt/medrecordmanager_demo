using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedRecordManager.Models
    {

        public class RuleModel
        {

            public RuleModel()
            {
                Definition = new List<RuleItem>();
            }
            public int Id { get; set; }

            public string RuleName { get; set; }

            public string Description { get; set; }

            public bool Enabled { get; set; }

            public IList<RuleItem> Definition { get; set; }

            public int RuleSetCount { get { return Definition.Count(); } }
        }

        public class RuleItem
        {
            public string LogicOperator { get; set; }
            public string Operator { get; set; }

            public string Openparenthese { get; set; }

            public string Closeparenthese { get; set; }

            public string Field { get; set; }

            public string FieldValue { get; set; }
        }
    }




