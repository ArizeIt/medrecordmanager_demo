using System;

namespace MedRecordManager.Models.QueueRecord
{
    public class ReviewQueue
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public bool HasChild { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedTime { get; set; }

    }
}
