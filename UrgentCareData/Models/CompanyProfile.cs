using System.Collections.Generic;



namespace UrgentCareData.Models
{
    public partial class CompanyProfile
    {
        public CompanyProfile()
        {
            UserCompany = new HashSet<UserCompany>();
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
        public string DbConnection { get; set; }
        public string WebApiUri { get; set; }

        public virtual ICollection<UserCompany> UserCompany { get; set; }
    }
}
