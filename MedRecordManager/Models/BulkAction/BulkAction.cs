using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedRecordManager.Models
{
    public class BulkAction
    {
        public string ActionName { get; set; }
        public IDictionary<string, string> ActionSteps { get; set;}
    }

    
}
