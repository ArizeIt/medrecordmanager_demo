using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Requests
{
    [XmlRoot(ElementName = "ppmdmsg")]
    public class LookUpRequest
    {
        [XmlAttribute(AttributeName = "action")]
        public string Action { get; set; }
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
        [XmlAttribute(AttributeName = "msgtime")]
        public string Msgtime { get; set; }

        [XmlAttribute(AttributeName = "page")]
        public string Page { get; set; }
    }

    [XmlRoot(ElementName = "ppmdmsg")]
    public class PpmLookUpPatientRequest : LookUpRequest
    {
        [XmlAttribute(AttributeName = "ssn")]
        public string Ssn { get; set; }

        [XmlAttribute(AttributeName = "chart")]
        public string ChartNo { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

    }


    [XmlRoot(ElementName = "ppmdmsg")]
    public class PpmLookUpResPartyRequest : LookUpRequest
    {
        [XmlAttribute(AttributeName = "ssn")]
        public string Ssn { get; set; }

        [XmlAttribute(AttributeName = "dob")]
        public string Dob { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "acctnum")]
        public string Account { get; set;}
    }


    [XmlRoot(ElementName = "ppmdmsg")]
    public class PpmLookUpProviderRequest : LookUpRequest
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }
    }

    [XmlRoot(ElementName = "ppmdmsg")]
    public class PpmLookUpFinClassRequest :LookUpRequest
    {
        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }
    [XmlRoot(ElementName = "ppmdmsg")]
    public class PpmLookUpAcctTypeRequest : LookUpRequest
    {
        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "ppmdmsg")]
    public class PpmLookUpDiagCodeRequest : LookUpRequest
    {
        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }

        [XmlAttribute(AttributeName = "codeset")]
        public string Codeset { get; set; }

        [XmlAttribute(AttributeName = "exactmatch")]
        public string Exactmatch { get; set; }
    }


    [XmlRoot(ElementName = "ppmdmsg")]
    public class PpmLookUpProcCodeRequest : LookUpRequest
    {
        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }
    }


}
