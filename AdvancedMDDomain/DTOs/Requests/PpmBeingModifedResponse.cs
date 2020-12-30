using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Requests
{

    [XmlRoot(ElementName = "extrainfo")]
    public class Extrainfo
    {
        [XmlElement(ElementName = "requestinfo")]
        public string Requestinfo { get; set; }
    }

    [XmlRoot(ElementName = "detail")]
    public class Detail
    {
        [XmlElement(ElementName = "code")]
        public string Code { get; set; }
        [XmlElement(ElementName = "description")]
        public string Description { get; set; }
        [XmlElement(ElementName = "class")]
        public string Class { get; set; }
        [XmlElement(ElementName = "method")]
        public string Method { get; set; }
        [XmlElement(ElementName = "linenum")]
        public string Linenum { get; set; }
        [XmlElement(ElementName = "source")]
        public string Source { get; set; }
        [XmlElement(ElementName = "extrainfo")]
        public Extrainfo Extrainfo { get; set; }
    }

    [XmlRoot(ElementName = "Fault")]
    public class Fault
    {
        [XmlElement(ElementName = "faultcode")]
        public string Faultcode { get; set; }
        [XmlElement(ElementName = "faultstring")]
        public string Faultstring { get; set; }
        [XmlElement(ElementName = "detail")]
        public Detail Detail { get; set; }
    }

    [XmlRoot(ElementName = "Error")]
    public class Error
    {
        [XmlElement(ElementName = "Fault")]
        public Fault Fault { get; set; }
    }

    [XmlRoot(ElementName = "PPMDResults")]
    public class PpmBeingModifedResponse : IPpmResponse
    {
        [XmlElement(ElementName = "Results")]
        public string Results { get; set; }
        [XmlElement(ElementName = "Error")]
        public Error Error { get; set; }
    }
}
