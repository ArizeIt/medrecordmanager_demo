using System;
using System.Collections.Generic;

namespace UrgentCareData.Models
{
    public class CodeReviewQueue
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public int? ParentId { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedTime { get; set; }

        public virtual CodeReviewQueue ParentQueue { get; set; }

        public virtual ICollection<CodeReviewQueue> ChildrenQueue { get; set; }
    }
}
