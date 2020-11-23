using System;
using System.Collections.Generic;



namespace UrgentCareData.Models
{
    public partial class UserCompany
    {
        public string UserId { get; set; }
        public int CompanyId { get; set; }
        public int Id { get; set; }

        public virtual CompanyProfile Company { get; set; }
        public virtual User User { get; set; }
    }
}
