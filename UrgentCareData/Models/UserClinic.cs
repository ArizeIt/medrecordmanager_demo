using System;
using System.Collections.Generic;



namespace UrgentCareData.Models
{
    public partial class UserClinic
    {
        public string UserId { get; set; }
        public string ClinicId { get; set; }
        public int Id { get; set; }

        public virtual ClinicProfile Clinic { get; set; }
        public virtual User User { get; set; }
    }
}
