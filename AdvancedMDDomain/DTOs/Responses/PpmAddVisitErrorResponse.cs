using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Responses
{


    [XmlRoot(ElementName = "detail")]
    public class AddVisitDetail
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
        public string Extrainfo { get; set; }
    }

    [XmlRoot(ElementName = "Fault")]
    public class AddVisitFault
    {
        [XmlElement(ElementName = "faultcode")]
        public string Faultcode { get; set; }
        [XmlElement(ElementName = "faultstring")]
        public string Faultstring { get; set; }
        [XmlElement(ElementName = "detail")]
        public Detail Detail { get; set; }
    }

    [XmlRoot(ElementName = "Error")]
    public class AddVisitError
    {
        [XmlElement(ElementName = "Fault")]
        public Fault Fault { get; set; }
    }

    [XmlRoot(ElementName = "PPMDResults")]
    public class PPMVisitErrorResponse : IPpmResponse
    {
        [XmlElement(ElementName = "Results")]
        public string Results { get; set; }
        [XmlElement(ElementName = "Error")]
        public AddVisitError Error { get; set; }
        [XmlAttribute(AttributeName = "s")]
        public string S { get; set; }
        [XmlAttribute(AttributeName = "lst")]
        public string Lst { get; set; }
    }
}

