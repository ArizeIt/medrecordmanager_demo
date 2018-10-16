using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Responses
{
    [XmlRoot(ElementName = "proccode")]
    public class Proccode
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

    [XmlRoot(ElementName = "proccodelist")]
    public class Proccodelist
    {
        [XmlElement(ElementName = "proccode")]
        public Proccode Proccode { get; set; }
    }

    [XmlRoot(ElementName = "Results")]
    public class ProcCodeResults
    {
        [XmlElement(ElementName = "proccodelist")]
        public Proccodelist Proccodelist { get; set; }
    }

    [XmlRoot(ElementName = "PPMDResults")]
    public class PpmLookUpProcCodeResponse : IPpmResponse
    {
        [XmlElement(ElementName = "Results")]
        public ProcCodeResults Results { get; set; }
        [XmlElement(ElementName = "Error")]
        public string Error { get; set; }
        [XmlAttribute(AttributeName = "s")]
        public string S { get; set; }
        [XmlAttribute(AttributeName = "lst")]
        public string Lst { get; set; }
    }
   
}
