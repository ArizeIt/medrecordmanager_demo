using System;
using System.Collections.Generic;

namespace UrgentCareData.Models
{
    public partial class EntityChangeHistory
    {
        public long ChangeHistoryId { get; set; }
        public long VisitId { get; set; }
        public string OriginalValue { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifedEntityName { get; set; }
    }
}
