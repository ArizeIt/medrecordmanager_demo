using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Requests
{
    [XmlRoot(ElementName = "address")]
    public class UpdatePatientAddress
    {
        [XmlAttribute(AttributeName = "o_address1")]
        public string O_address1 { get; set; }
        [XmlAttribute(AttributeName = "address1")]
        public string Address1 { get; set; }
        [XmlAttribute(AttributeName = "o_address2")]
        public string O_address2 { get; set; }
        [XmlAttribute(AttributeName = "address2")]
        public string Address2 { get; set; }
        [XmlAttribute(AttributeName = "zip")]
        public string Zip { get; set; }
        [XmlAttribute(AttributeName = "o_zip")]
        public string O_zip { get; set; }
        [XmlAttribute(AttributeName = "city")]
        public string City { get; set; }
        [XmlAttribute(AttributeName = "o_city")]
        public string O_city { get; set; }
        [XmlAttribute(AttributeName = "o_state")]
        public string O_state { get; set; }
        [XmlAttribute(AttributeName = "state")]
        public string State { get; set; }
    }

    [XmlRoot(ElementName = "contactinfo")]
    public class UpdateContactinfo
    {
        [XmlAttribute(AttributeName = "o_homephone")]
        public string O_homephone { get; set; }
        [XmlAttribute(AttributeName = "homephone")]
        public string Homephone { get; set; }
        [XmlAttribute(AttributeName = "o_preferredcommunicationfid")]
        public string O_preferredcommunicationfid { get; set; }
        [XmlAttribute(AttributeName = "preferredcommunicationfid")]
        public string Preferredcommunicationfid { get; set; }
    }

    [XmlRoot(ElementName = "patient")]
    public class UpdatePatient
    {
        [XmlElement(ElementName = "address")]
        public UpdatePatientAddress Address { get; set; }
        [XmlElement(ElementName = "contactinfo")]
        public UpdateContactinfo Contactinfo { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "o_ssn")]
        public string O_ssn { get; set; }
        [XmlAttribute(AttributeName = "ssn")]
        public string Ssn { get; set; }
        [XmlAttribute(AttributeName = "o_finclass")]
        public string O_finclass { get; set; }
        [XmlAttribute(AttributeName = "finclass")]
        public string finclass { get; set; }
        [XmlAttribute(AttributeName = "o_profile")]
        public string O_profile { get; set; }
        [XmlAttribute(AttributeName = "profile")]
        public string Profile { get; set; }
        [XmlAttribute(AttributeName = "o_insorder")]
        public string O_insorder { get; set; }
        [XmlAttribute(AttributeName = "insorder")]
        public string Insorder { get; set; }

    }

    [XmlRoot(ElementName = "patientlist")]
    public class UpdatePatientlist
    {
        [XmlElement(ElementName = "patient")]
        public UpdatePatient Patient { get; set; }
    }

    [XmlRoot(ElementName = "familychanges")]
    public class UpdatePatientFamilychanges
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
        [XmlAttribute(AttributeName = "homephone")]
        public string Homephone { get; set; }
    }

    [XmlRoot(ElementName = "ppmdmsg")]
    public class PpmUpdatePatientRequest : IPpmRequest
    {
        [XmlElement(ElementName = "patientlist")]
        public UpdatePatientlist Patientlist { get; set; }
        [XmlElement(ElementName = "familychanges")]
        public UpdatePatientFamilychanges Familychanges { get; set; }
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
