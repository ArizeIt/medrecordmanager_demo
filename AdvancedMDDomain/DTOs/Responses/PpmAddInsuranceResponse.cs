using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Responses
{
    [XmlRoot(ElementName = "patient")]
    public class InsResponsePatient
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "sequence")]
        public string Sequence { get; set; }
        [XmlAttribute(AttributeName = "insnotefid")]
        public string Insnotefid { get; set; }
    }

    [XmlRoot(ElementName = "Results")]
    public class AddInsuranceResults
    {
        [XmlElement(ElementName = "patient")]
        public InsResponsePatient Patient { get; set; }
        [XmlAttribute(AttributeName = "success")]
        public string Success { get; set; }
    }

    [XmlRoot(ElementName = "PPMDResults")]
    public class PpmAddInsuranceResponse : IPpmResponse
    {
        [XmlElement(ElementName = "Results")]
        public AddInsuranceResults Results { get; set; }
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
