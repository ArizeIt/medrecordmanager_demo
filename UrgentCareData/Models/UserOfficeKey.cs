using System;
using System.Collections.Generic;



namespace UrgentCareData.Models
{
    public partial class UserOfficeKey
    {
        public string UserId { get; set; }
        public int OfficeKey { get; set; }
        public int Id { get; set; }

        public virtual User User { get; set; }
    }
}
