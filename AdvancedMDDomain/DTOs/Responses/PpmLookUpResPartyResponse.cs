using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Responses
{
    [XmlRoot(ElementName = "address")]
    public class LookUpAddress
    {
        [XmlAttribute(AttributeName = "zipid")]
        public string Zipid { get; set; }
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
        public string Countrycode { get; set; }
        [XmlAttribute(AttributeName = "areacode")]
        public string Areacode { get; set; }
    }

    [XmlRoot(ElementName = "contactinfo")]
    public class LookUpContactinfo
    {
        [XmlAttribute(AttributeName = "homephone")]
        public string Homephone { get; set; }
    }

    [XmlRoot(ElementName = "respparty")]
    public class LookUpRespparty
    {
        [XmlElement(ElementName = "address")]
        public LookUpAddress Address { get; set; }
        [XmlElement(ElementName = "contactinfo")]
        public LookUpContactinfo Contactinfo { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "acctnum")]
        public string Acctnum { get; set; }
        [XmlAttribute(AttributeName = "ssn")]
        public string Ssn { get; set; }
        [XmlAttribute(AttributeName = "dob")]
        public string Dob { get; set; }
    }

    [XmlRoot(ElementName = "resppartylist")]
    public class LookUpResppartylist
    {
        [XmlElement(ElementName = "respparty")]
        public LookUpRespparty Respparty { get; set; }
        [XmlAttribute(AttributeName = "itemcount")]
        public string Itemcount { get; set; }
        [XmlAttribute(AttributeName = "page")]
        public string Page { get; set; }
        [XmlAttribute(AttributeName = "pagecount")]
        public string Pagecount { get; set; }
        [XmlAttribute(AttributeName = "itemsfrom")]
        public string Itemsfrom { get; set; }
        [XmlAttribute(AttributeName = "itemsto")]
        public string Itemsto { get; set; }
    }

    [XmlRoot(ElementName = "Results")]
    public class RespartyResults
    {
        [XmlElement(ElementName = "resppartylist")]
        public LookUpResppartylist Resppartylist { get; set; }
    }

    [XmlRoot(ElementName = "PPMDResults")]
    public class PpmLookUpResPartyResponse : IPpmResponse
    {
        [XmlElement(ElementName = "Results")]
        public RespartyResults Results { get; set; }
        [XmlElement(ElementName = "Error")]
        public string Error { get; set; }
        [XmlAttribute(AttributeName = "s")]
        public string S { get; set; }
        [XmlAttribute(AttributeName = "lst")]
        public string Lst { get; set; }
    }

}
