using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedMDDomain.Lookups

{
    public class RequestAction
    {
        private RequestAction(string value) { Value = value; }

        public string Value { get; set; }

        public static RequestAction LookUpPatient => new RequestAction("lookuppatient");

        public static RequestAction LookupProfile => new RequestAction("lookupprofile");

        public static RequestAction LookupFinClass => new RequestAction("lookupfinclass");

        public static RequestAction LookupAcctType => new RequestAction("lookupaccttype");

        public static RequestAction LookupDiagCode => new RequestAction("lookupdiagcode");

        public static RequestAction LookupResParty => new RequestAction("lookuprespparty");

        public static RequestAction Login => new RequestAction("login");

        public static RequestAction AddVisit => new RequestAction("addvisit");
        public static RequestAction AddPatient => new RequestAction("addpatient");

        public static RequestAction AddInsurance => new RequestAction("addinsurance");
        public static RequestAction UpdatePatient => new RequestAction("updatepatient");

        public static RequestAction UpdateRespParty => new RequestAction("updaterespparty");

        public static RequestAction AddResparty => new RequestAction("addrespparty");

        public static RequestAction LookUpProcCode => new RequestAction("lookupproccode");

        public static RequestAction LookUpCarrier => new RequestAction("lookupcarrier");

        public static RequestAction UploadFile => new RequestAction("uploadfile");

        public static RequestAction SaveCharges=> new RequestAction("savecharges");

        public static RequestAction UpdateVisitWithCharges => new RequestAction("updvisitwithnewcharges");

        public static RequestAction GetFees=> new RequestAction("getfees");

        public static  RequestAction GetEpisodes=> new RequestAction("getepisodes");

        public static RequestAction NewBatch => new RequestAction("newbatch");

        public static RequestAction AddPayment => new RequestAction("addpayments");

        public static RequestAction GetDemographic => new RequestAction("getdemographic");
        public static RequestAction SavePatientNote => new RequestAction("savepatientnotes");
    }
}
