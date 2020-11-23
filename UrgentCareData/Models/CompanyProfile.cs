using System;
using System.Collections.Generic;



namespace UrgentCareData.Models
{
    public partial class CompanyProfile
    {
        public CompanyProfile()
        {
            CompanyClinics = new HashSet<CompanyClinic>();
            UserCompanies = new HashSet<UserCompany>();
        }

        public string CompanyName { get; set; }
        public int Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }

        public virtual ICollection<CompanyClinic> CompanyClinics { get; set; }
        public virtual ICollection<UserCompany> UserCompanies { get; set; }
    }
}
