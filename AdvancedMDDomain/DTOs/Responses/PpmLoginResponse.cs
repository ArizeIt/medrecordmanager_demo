using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Responses
{

    [XmlRoot(ElementName = "usercontext")]
    public class Usercontext
    {

        [XmlAttribute(AttributeName = "reportingServerURL")]
        public string ReportingServerURL { get; set; }

        [XmlAttribute(AttributeName = "adhocReportingURL")]
        public string AdhocReportingURL { get; set; }

        [XmlAttribute(AttributeName = "aibenchmarksurl")]
        public string Aibenchmarksurl { get; set; }

        [XmlAttribute(AttributeName = "helpurl")]
        public string Helpurl { get; set; }

        [XmlAttribute(AttributeName = "webserver")]
        public string Webserver { get; set; }

        [XmlAttribute(AttributeName = "username")]
        public string Username { get; set; }

        [XmlAttribute(AttributeName = "email")]
        public string Email { get; set; }

        [XmlAttribute(AttributeName = "checkemail")]
        public string Checkemail { get; set; }

        [XmlAttribute(AttributeName = "needsconfirmation")]
        public string Needsconfirmation { get; set; }

        [XmlAttribute(AttributeName = "sendnewconfirmation")]
        public string Sendnewconfirmation { get; set; }

        [XmlAttribute(AttributeName = "pmredirecturl")]
        public string Pmredirecturl { get; set; }

        [XmlAttribute(AttributeName = "ehrredirecturl")]
        public string Ehrredirecturl { get; set; }

        [XmlAttribute(AttributeName = "telemedicineapiurl")]
        public string Telemedicineapiurl { get; set; }
        [XmlAttribute(AttributeName = "capturechallengesettings")]
        public string Capturechallengesettings { get; set; }
        [XmlAttribute(AttributeName = "providerid")]
        public string Providerid { get; set; }

        [XmlAttribute(AttributeName = "defaultprofileid")]
        public string Defaultprofileid { get; set; }

        [XmlAttribute(AttributeName = "departmentid")]
        public string Departmentid { get; set; }

        [XmlAttribute(AttributeName = "clinicaluser")]
        public string Clinicaluser { get; set; }

        [XmlAttribute(AttributeName = "roleid")]
        public string Roleid { get; set; }

        [XmlAttribute(AttributeName = "officecode")]
        public string Officecode { get; set; }

        [XmlAttribute(AttributeName = "logincode")]
        public string Logincode { get; set; }

        [XmlAttribute(AttributeName = "officename")]
        public string Officename { get; set; }

        [XmlAttribute(AttributeName = "admin")]
        public string Admin { get; set; }

        [XmlAttribute(AttributeName = "status")]
        public string Status { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "variation")]
    public class Variation
    {
        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }

        [XmlAttribute(AttributeName = "configfilename")]
        public string Configfilename { get; set; }
    }

    [XmlRoot(ElementName = "Results")]
    public class LoginResponseResults
    {
        [XmlElement(ElementName = "usercontext")]
        public Usercontext Usercontext { get; set; }

        [XmlElement(ElementName = "variation")]
        public Variation Variation { get; set; }

        [XmlAttribute(AttributeName = "success")]
        public string Success { get; set; }

        [XmlAttribute(AttributeName = "api")]
        public string Api { get; set; }

        [XmlAttribute(AttributeName = "version")]
        public string Version { get; set; }

    }

    [XmlRoot(ElementName = "PPMDResults")]
    public class PpmLoginResponse : IPpmResponse
    {
        [XmlElement(ElementName = "Results")]
        public LoginResponseResults Results { get; set; }

        [XmlElement(ElementName = "Error")]
        public string Error { get; set; }

        [XmlAttribute(AttributeName = "s")]
        public string S { get; set; }

        [XmlAttribute(AttributeName = "lst")]
        public string Lst { get; set; }

    }
}
