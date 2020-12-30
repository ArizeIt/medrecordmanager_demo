using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Requests
{
    [XmlRoot(ElementName = "address")]
    public class AddAddress
    {
        [XmlAttribute(AttributeName = "zip")]
        public string Zip { get; set; }
        [XmlAttribute(AttributeName = "countrycode")]
        public string Countrycode { get; set; }
        [XmlAttribute(AttributeName = "city")]
        public string City { get; set; }
        [XmlAttribute(AttributeName = "state")]
        public string State { get; set; }
        [XmlAttribute(AttributeName = "address1")]
        public string Address1 { get; set; }
        [XmlAttribute(AttributeName = "address2")]
        public string Address2 { get; set; }
    }

    [XmlRoot(ElementName = "contactinfo")]
    public class AddContactinfo
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
        [XmlAttribute(AttributeName = "preferredcommunicationfid")]
        public string Preferredcommunicationfid { get; set; }
        [XmlAttribute(AttributeName = "communicationnote")]
        public string Communicationnote { get; set; }
        [XmlAttribute(AttributeName = "email")]
        public string Email { get; set; }
    }

    [XmlRoot(ElementName = "respparty")]
    public class AddRespparty
    {
        [XmlElement(ElementName = "address")]
        public AddAddress Address { get; set; }
        [XmlElement(ElementName = "contactinfo")]
        public AddContactinfo Contactinfo { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "dob")]
        public string Dob { get; set; }
        [XmlAttribute(AttributeName = "sex")]
        public string Sex { get; set; }
        [XmlAttribute(AttributeName = "employer")]
        public string Employer { get; set; }
        [XmlAttribute(AttributeName = "employstatus")]
        public string Employstatus { get; set; }
        [XmlAttribute(AttributeName = "ssn")]
        public string Ssn { get; set; }
        [XmlAttribute(AttributeName = "accttype")]
        public string Accttype { get; set; }
        [XmlAttribute(AttributeName = "title")]
        public string Title { get; set; }
        [XmlAttribute(AttributeName = "sendstmt")]
        public string Sendstmt { get; set; }
        [XmlAttribute(AttributeName = "stmtrestart")]
        public string Stmtrestart { get; set; }
        [XmlAttribute(AttributeName = "stmtformat")]
        public string Stmtformat { get; set; }
        [XmlAttribute(AttributeName = "billcycle")]
        public string Billcycle { get; set; }
        [XmlAttribute(AttributeName = "fincharge")]
        public string Fincharge { get; set; }
    }

    [XmlRoot(ElementName = "ppmdmsg")]
    public class PpmAddResPartyRequest : IPpmRequest
    {
        [XmlElement(ElementName = "respparty")]
        public AddRespparty Respparty { get; set; }
        [XmlElement(ElementName = "familychanges")]
        public string Familychanges { get; set; }
        [XmlAttribute(AttributeName = "action")]
        public string Action { get; set; }
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
        [XmlAttribute(AttributeName = "msgtime")]
        public string Msgtime { get; set; }
        [XmlAttribute(AttributeName = "ltq")]
        public string Ltq { get; set; }
        [XmlAttribute(AttributeName = "la")]
        public string La { get; set; }
        [XmlAttribute(AttributeName = "lac")]
        public string Lac { get; set; }
        [XmlAttribute(AttributeName = "lat")]
        public string Lat { get; set; }
        [XmlAttribute(AttributeName = "let")]
        public string Let { get; set; }
        [XmlAttribute(AttributeName = "lst")]
        public string Lst { get; set; }
    }
}
