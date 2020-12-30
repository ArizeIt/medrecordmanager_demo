using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Requests
{
    [XmlRoot(ElementName = "address")]
    public class UpdateAddress
    {
        [XmlAttribute(AttributeName = "o_zip")]
        public string OldZip { get; set; }
        [XmlAttribute(AttributeName = "o_city")]
        public string OldCity { get; set; }
        [XmlAttribute(AttributeName = "o_state")]
        public string OldState { get; set; }
        [XmlAttribute(AttributeName = "o_address1")]
        public string Oldaddress1 { get; set; }
        [XmlAttribute(AttributeName = "o_address2")]
        public string OldAddress2 { get; set; }
        [XmlAttribute(AttributeName = "o_countrycode")]
        public string OldCountryCode { get; set; }

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
        [XmlAttribute(AttributeName = "countrycode")]
        public string CountryCode { get; set; }
    }

    [XmlRoot(ElementName = "contactinfo")]
    public class UpdateContactInfo
    {
        [XmlAttribute(AttributeName = "o_homephone")]
        public string OldHomephone { get; set; }
        [XmlAttribute(AttributeName = "o_officephone")]
        public string OldOfficephone { get; set; }
        [XmlAttribute(AttributeName = "o_officeext")]
        public string OldOfficeext { get; set; }
        [XmlAttribute(AttributeName = "o_otherphone")]
        public string OldOtherphone { get; set; }
        [XmlAttribute(AttributeName = "o_othertype")]
        public string OldOthertype { get; set; }
        [XmlAttribute(AttributeName = "o_email")]
        public string OldEmail { get; set; }


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

    [XmlRoot(ElementName = "respparty")]
    public class UpdateRespparty
    {
        [XmlElement(ElementName = "address")]
        public UpdateAddress Address { get; set; }
        [XmlElement(ElementName = "contactinfo")]
        public UpdateContactInfo Contactinfo { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "title")]
        public string Title { get; set; }
        [XmlAttribute(AttributeName = "sex")]
        public string Sex { get; set; }
        [XmlAttribute(AttributeName = "dob")]
        public string Dob { get; set; }
        [XmlAttribute(AttributeName = "ssn")]
        public string Ssn { get; set; }
    }

    [XmlRoot(ElementName = "familychanges")]
    public class Familychanges
    {
        [XmlAttribute(AttributeName = "address1")]
        public string Address1 { get; set; }
        [XmlAttribute(AttributeName = "address2")]
        public string Address2 { get; set; }
        [XmlAttribute(AttributeName = "zip")]
        public string Zip { get; set; }
        [XmlAttribute(AttributeName = "city")]
        public string City { get; set; }
        [XmlAttribute(AttributeName = "state")]
        public string State { get; set; }
    }

    [XmlRoot(ElementName = "ppmdmsg")]
    public class PpmUpdateRespPartyRequest : IPpmRequest
    {
        [XmlElement(ElementName = "respparty")]
        public UpdateRespparty Respparty { get; set; }
        [XmlElement(ElementName = "familychanges")]
        public Familychanges Familychanges { get; set; }
        [XmlAttribute(AttributeName = "action")]
        public string Action { get; set; }
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
        [XmlAttribute(AttributeName = "msgtime")]
        public string Msgtime { get; set; }
        [XmlAttribute(AttributeName = "force")]
        public string Force { get; set; }
        //[XmlAttribute(AttributeName = "ltq")]
        //public string Ltq { get; set; }
        //[XmlAttribute(AttributeName = "la")]
        //public string La { get; set; }
        //[XmlAttribute(AttributeName = "lac")]
        //public string Lac { get; set; }
        //[XmlAttribute(AttributeName = "lat")]
        //public string Lat { get; set; }
        //[XmlAttribute(AttributeName = "let")]
        //public string Let { get; set; }
        //[XmlAttribute(AttributeName = "lst")]
        //public string Lst { get; set; }
    }

}
