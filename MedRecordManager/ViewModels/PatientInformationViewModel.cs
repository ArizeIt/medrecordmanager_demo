using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedRecordManager.ViewModels
{
    public class PatientInformationViewModel
    {
        public int Pat_Num { get; set; }
        public string SSN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Birthday { get; set; }
        public string Sex { get; set; }
        public string CellPhone { get; set; }
        public string HomePhone { get; set; }
        public string Email { get; set; }
    }
}
