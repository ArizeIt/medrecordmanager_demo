using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Requests
{
    [XmlRoot(ElementName = "ppmdmsg")]
    public class PpmAddNewBatchRequest : IPpmRequest
    {
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
