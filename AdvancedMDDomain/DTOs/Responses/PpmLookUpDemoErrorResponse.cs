using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Responses
{

    [XmlRoot(ElementName = "detail")]
    public class DemoDetail
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
    public class DemoFault
    {
        [XmlElement(ElementName = "faultcode")]
        public string Faultcode { get; set; }
        [XmlElement(ElementName = "faultstring")]
        public string Faultstring { get; set; }
        [XmlElement(ElementName = "detail")]
        public DemoDetail Detail { get; set; }
    }

    [XmlRoot(ElementName = "Error")]
    public class DemoError
    {
        [XmlElement(ElementName = "Fault")]
        public DemoFault Fault { get; set; }
    }

    [XmlRoot(ElementName = "PPMDResults")]
    public class PpmLookUpDemoErrorResponse
    {
        [XmlElement(ElementName = "Results")]
        public string Results { get; set; }
        [XmlElement(ElementName = "Error")]
        public DemoError Error { get; set; }
        [XmlAttribute(AttributeName = "s")]
        public string S { get; set; }
        [XmlAttribute(AttributeName = "lst")]
        public string Lst { get; set; }
        [XmlAttribute(AttributeName = "n")]
        public string N { get; set; }
    }
}
