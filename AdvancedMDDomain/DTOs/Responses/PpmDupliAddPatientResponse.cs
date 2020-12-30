using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Responses
{
    [XmlRoot(ElementName = "Results")]
    public class DuplicatePatientResults
    {
        [XmlAttribute(AttributeName = "success")]
        public string Success { get; set; }
    }


    [XmlRoot(ElementName = "PPMDResults")]
    public class PpmDupliAddPatientResponse : IPpmResponse
    {
        [XmlElement(ElementName = "Results")]
        public DuplicatePatientResults Results { get; set; }
        [XmlElement(ElementName = "Error")]
        public Error Error { get; set; }
        [XmlAttribute(AttributeName = "s")]
        public string S { get; set; }
        [XmlAttribute(AttributeName = "lst")]
        public string Lst { get; set; }
    }

}
