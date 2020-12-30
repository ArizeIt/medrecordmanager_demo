using System.Collections.Generic;
using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Responses
{

    [XmlRoot(ElementName = "address")]
    public class CarrierAddress
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
        [XmlAttribute(AttributeName = "areacode")]
        public string Areacode { get; set; }
    }

    [XmlRoot(ElementName = "carrier")]
    public class Carrier
    {
        [XmlElement(ElementName = "address")]
        public CarrierAddress Address { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "carrierlist")]
    public class Carrierlist
    {
        [XmlElement(ElementName = "carrier")]
        public List<Carrier> Carrier { get; set; }
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
    public class LookUpCarrierResults
    {
        [XmlElement(ElementName = "carrierlist")]
        public Carrierlist Carrierlist { get; set; }
    }

    [XmlRoot(ElementName = "PPMDResults")]
    public class PpmLookUpCarrierResponse : IPpmResponse
    {
        [XmlElement(ElementName = "Results")]
        public LookUpCarrierResults Results { get; set; }
        [XmlElement(ElementName = "Error")]
        public string Error { get; set; }
        [XmlAttribute(AttributeName = "s")]
        public string S { get; set; }
        [XmlAttribute(AttributeName = "lst")]
        public string Lst { get; set; }
    }
}
