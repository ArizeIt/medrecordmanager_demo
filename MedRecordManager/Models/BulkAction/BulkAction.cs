using System.Collections.Generic;

namespace MedRecordManager.Models
{
    public class BulkAction
    {
        public string ActionName { get; set; }
        public IDictionary<string, string> ActionSteps { get; set; }
    }


}
