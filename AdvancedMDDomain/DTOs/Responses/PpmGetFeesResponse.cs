using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Responses
{
    [XmlRoot(ElementName = "proccode")]
    public class GetFeeProccode
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }
        [XmlAttribute(AttributeName = "units")]
        public string Units { get; set; }
        [XmlAttribute(AttributeName = "pos")]
        public string Pos { get; set; }
        [XmlAttribute(AttributeName = "tos")]
        public string Tos { get; set; }
        [XmlAttribute(AttributeName = "fee")]
        public string Fee { get; set; }
        [XmlAttribute(AttributeName = "allowable")]
        public string Allowable { get; set; }
        [XmlAttribute(AttributeName = "billins")]
        public string Billins { get; set; }
        [XmlAttribute(AttributeName = "billpat")]
        public string Billpat { get; set; }
        [XmlAttribute(AttributeName = "dollarcopay")]
        public string Dollarcopay { get; set; }
        [XmlAttribute(AttributeName = "tax")]
        public string Tax { get; set; }
        [XmlAttribute(AttributeName = "ndc")]
        public string Ndc { get; set; }
        [XmlAttribute(AttributeName = "ndcmeasurename")]
        public string Ndcmeasurename { get; set; }
        [XmlAttribute(AttributeName = "ndcmeasurefid")]
        public string Ndcmeasurefid { get; set; }
        [XmlAttribute(AttributeName = "ndcunitprice")]
        public string Ndcunitprice { get; set; }
        [XmlAttribute(AttributeName = "ndcquantity")]
        public string Ndcquantity { get; set; }
        [XmlAttribute(AttributeName = "userfileformfid")]
        public string Userfileformfid { get; set; }
    }

    [XmlRoot(ElementName = "proccodelist")]
    public class GetFeesProccodelist
    {
        [XmlElement(ElementName = "proccode")]
        public GetFeeProccode Proccode { get; set; }
    }

    [XmlRoot(ElementName = "Results")]
    public class GetFeesResults
    {
        [XmlElement(ElementName = "proccodelist")]
        public GetFeesProccodelist Proccodelist { get; set; }
    }

    [XmlRoot(ElementName = "PPMDResults")]
    public class PpmGetFeesResponse : IPpmResponse
    {
        [XmlElement(ElementName = "Results")]
        public GetFeesResults Results { get; set; }
        [XmlElement(ElementName = "Error")]
        public string Error { get; set; }
        [XmlAttribute(AttributeName = "s")]
        public string S { get; set; }
        [XmlAttribute(AttributeName = "lst")]
        public string Lst { get; set; }
        [XmlAttribute(AttributeName = "n")]
        public string N { get; set; }
    }
}
