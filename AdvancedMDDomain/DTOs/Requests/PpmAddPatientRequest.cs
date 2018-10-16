using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Requests
{
    [XmlRoot(ElementName = "address")]
    public class Address
    {
        [XmlAttribute(AttributeName = "zip")]
        public string Zip { get; set; }
        [XmlAttribute(AttributeName = "city")]
        public string City { get; set; }
        [XmlAttribute(AttributeName = "state")]
        public string State { get; set; }
        [XmlAttribute(AttributeName = "address1")]
        public string Address1 { get; set; }
        [XmlAttribute(AttributeName = "address2")]
        public string Address2 { get; set; }
        [XmlAttribute(AttributeName ="countrycode")]
        public string CountryCode { get; set; }
    }

    [XmlRoot(ElementName = "contactinfo")]
    public class Contactinfo
    {
        [XmlAttribute(AttributeName = "homephone")]
        public string Homephone { get; set; }
        [XmlAttribute(AttributeName = "officephone")]
        public string Officephone { get; set; }
        [XmlAttribute(AttributeName = "officeext")]
        public string Officeext { get; set; }
        [XmlAttribute(AttributeName = "otherphone")]
        public string Otherphone { get; set; }
        [XmlAttribute(AttributeName = "othertype")]
        public string Othertype { get; set; }
        [XmlAttribute(AttributeName = "email")]
        public string Email { get; set; }
    }

    [XmlRoot(ElementName = "patient")]
    public class Patient
    {
        [XmlElement(ElementName = "address")]
        public Address Address { get; set; }
        [XmlElement(ElementName = "contactinfo")]
        public Contactinfo Contactinfo { get; set; }
        [XmlAttribute(AttributeName = "respparty")]
        public string Respparty { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "sex")]
        public string Sex { get; set; }
        [XmlAttribute(AttributeName = "relationship")]
        public string Relationship { get; set; }
        [XmlAttribute(AttributeName = "dob")]
        public string Dob { get; set; }
        [XmlAttribute(AttributeName = "ssn")]
        public string Ssn { get; set; }
        [XmlAttribute(AttributeName = "chart")]
        public string Chart { get; set; }
        [XmlAttribute(AttributeName = "profile")]
        public string Profile { get; set; }
        [XmlAttribute(AttributeName = "finclass")]
        public string Finclass { get; set; }
        [XmlAttribute(AttributeName = "deceased")]
        public string Deceased { get; set; }
        [XmlAttribute(AttributeName = "title")]
        public string Title { get; set; }
        [XmlAttribute(AttributeName = "maritalstatus")]
        public string Maritalstatus { get; set; }
        [XmlAttribute(AttributeName = "insorder")]
        public string Insorder { get; set; }
        [XmlAttribute(AttributeName = "employer")]
        public string Employer { get; set; }
        [XmlAttribute(AttributeName = "hipaarelationship")]
        public string HipaaRelationship { get; set; }
        [XmlAttribute(AttributeName = "force")]
        public string Force { get; set; }
    }

    [XmlRoot(ElementName = "patientlist")]
    public class RequestPatientlist
    {
        [XmlElement(ElementName = "patient")]
        public Patient Patient { get; set; }
    }

    [XmlRoot(ElementName = "respparty")]
    public class Respparty
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "accttype")]
        public string Accttype { get; set; }

        [XmlAttribute(AttributeName = "zip")]
        public string Zip { get; set; }
        [XmlAttribute(AttributeName = "city")]
        public string City { get; set; }
        [XmlAttribute(AttributeName = "state")]
        public string State { get; set; }
        [XmlAttribute(AttributeName = "address1")]
        public string Address1 { get; set; }
        [XmlAttribute(AttributeName = "address2")]
        public string Address2 { get; set; }
        [XmlAttribute(AttributeName = "phone")]
        public string Homephone { get; set; }

    }

    [XmlRoot(ElementName = "resppartylist")]
    public class Resppartylist
    {
        [XmlElement(ElementName = "respparty")]
        public Respparty Respparty { get; set; }
    }

    [XmlRoot(ElementName = "ppmdmsg")]
    public class PpmAddPatientRequest :IPpmRequest
    {
        
        [XmlElement(ElementName = "patientlist")]
        public RequestPatientlist RequestPatientlist { get; set; }
        [XmlElement(ElementName = "resppartylist")]
        public Resppartylist Resppartylist { get; set; }
        [XmlAttribute(AttributeName = "action")]
        public string Action { get; set; }
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
        [XmlAttribute(AttributeName = "msgtime")]
        public string Msgtime { get; set; }
        [XmlAttribute(AttributeName = "force")]
        public string Force { get; set; }
    }


}
