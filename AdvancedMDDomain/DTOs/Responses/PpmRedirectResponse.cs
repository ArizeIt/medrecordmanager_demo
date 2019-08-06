using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Responses
{
    [XmlRoot(ElementName = "usercontext")]
    public class RedirectUsercontext
    {
        [XmlAttribute(AttributeName = "webserver")]
        public string Webserver { get; set; }
        [XmlAttribute(AttributeName = "reportingServerURL")]
        public string ReportingServerURL { get; set; }
        [XmlAttribute(AttributeName = "helpurl")]
        public string Helpurl { get; set; }
        [XmlAttribute(AttributeName = "username")]
        public string Username { get; set; }
    }

    [XmlRoot(ElementName = "Results")]
    public class RedirectResults
    {
        [XmlElement(ElementName = "usercontext")]
        public RedirectUsercontext Usercontext { get; set; }
        [XmlAttribute(AttributeName = "success")]
        public string Success { get; set; }
    }

    [XmlRoot(ElementName = "detail")]
    public class RedirectDetail
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
    public class RedirectFault
    {
        [XmlElement(ElementName = "faultcode")]
        public string Faultcode { get; set; }
        [XmlElement(ElementName = "faultstring")]
        public string Faultstring { get; set; }
        [XmlElement(ElementName = "detail")]
        public RedirectDetail Detail { get; set; }
    }

    [XmlRoot(ElementName = "Error")]
    public class RedirectError
    {
        [XmlElement(ElementName = "Fault")]
        public RedirectFault Fault { get; set; }
    }

    [XmlRoot(ElementName = "PPMDResults")]
    public class PpmRedirectResponse : IPpmResponse

    {
        [XmlElement(ElementName = "Results")]
        public RedirectResults Results { get; set; }
        [XmlElement(ElementName = "Error")]
        public RedirectError Error { get; set; }
        [XmlAttribute(AttributeName = "s")]
        public string S { get; set; }
        [XmlAttribute(AttributeName = "lst")]
        public string Lst { get; set; }
    }

}
