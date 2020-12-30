using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Requests
{
    [XmlRoot(ElementName = "proccode")]
    public class RequestProccode
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "units")]
        public string Units { get; set; }
        [XmlAttribute(AttributeName = "ndccode")]
        public string Ndccode { get; set; }
        [XmlAttribute(AttributeName = "ndcquantity")]
        public string Ndcquantity { get; set; }
        [XmlAttribute(AttributeName = "ndcmeasure")]
        public string Ndcmeasure { get; set; }
        [XmlAttribute(AttributeName = "ndcmeasurefid")]
        public string Ndcmeasurefid { get; set; }
        [XmlAttribute(AttributeName = "ndcprice")]
        public string Ndcprice { get; set; }
        [XmlAttribute(AttributeName = "pos")]
        public string Pos { get; set; }
        [XmlAttribute(AttributeName = "tos")]
        public string Tos { get; set; }
        [XmlAttribute(AttributeName = "revcode")]
        public string Revcode { get; set; }
        [XmlAttribute(AttributeName = "revdesc")]
        public string Revdesc { get; set; }
        [XmlAttribute(AttributeName = "defaultglobaldays")]
        public string Defaultglobaldays { get; set; }
        [XmlAttribute(AttributeName = "macro")]
        public string Macro { get; set; }
    }

    [XmlRoot(ElementName = "ppmdmsg")]
    public class PpmGetFeesRequest : IPpmRequest
    {
        [XmlElement(ElementName = "proccode")]
        public RequestProccode Proccode { get; set; }
        [XmlAttribute(AttributeName = "action")]
        public string Action { get; set; }
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
        [XmlAttribute(AttributeName = "msgtime")]
        public string Msgtime { get; set; }
        [XmlAttribute(AttributeName = "ltq")]
        public string Ltq { get; set; }
        [XmlAttribute(AttributeName = "chargeschedid")]
        public string Chargeschedid { get; set; }
        [XmlAttribute(AttributeName = "allowschedid")]
        public string Allowschedid { get; set; }
        [XmlAttribute(AttributeName = "dos")]
        public string Dos { get; set; }
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
