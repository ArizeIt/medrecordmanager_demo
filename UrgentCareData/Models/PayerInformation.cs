﻿namespace UrgentCareData.Models
{
    public partial class PayerInformation
    {
        public int? PayerNum { get; set; }
        public string GroupId { get; set; }
        public string MemberId { get; set; }
        public int? Class { get; set; }
        public string Type { get; set; }
        public int? Priority { get; set; }
        public int PayerInfoId { get; set; }
        public int? InsuranceId { get; set; }
        public int VisitId { get; set; }
        public string InsName { get; set; }

        public virtual InsuranceInformation Insurance { get; set; }
        public virtual Visit Visit { get; set; }
    }
}
