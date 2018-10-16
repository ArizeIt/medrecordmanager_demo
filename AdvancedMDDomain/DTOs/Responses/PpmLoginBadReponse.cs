using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Responses
{
    [XmlRoot(ElementName = "ppmdmsg")]
    public class Ppmdmsg
    {
        [XmlAttribute(AttributeName = "action")]
        public string Action { get; set; }
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
        [XmlAttribute(AttributeName = "msgtime")]
        public string Msgtime { get; set; }
        [XmlAttribute(AttributeName = "nocookie")]
        public string Nocookie { get; set; }
        [XmlAttribute(AttributeName = "username")]
        public string Username { get; set; }
        [XmlAttribute(AttributeName = "psw")]
        public string Psw { get; set; }
        [XmlAttribute(AttributeName = "officecode")]
        public string Officecode { get; set; }
        [XmlAttribute(AttributeName = "appname")]
        public string Appname { get; set; }
    }

    [XmlRoot(ElementName = "requestinfo")]
    public class Requestinfo
    {
        [XmlElement(ElementName = "ppmdmsg")]
        public Ppmdmsg Ppmdmsg { get; set; }
    }

    [XmlRoot(ElementName = "extrainfo")]
    public class Extrainfo
    {
        [XmlElement(ElementName = "requestinfo")]
        public Requestinfo Requestinfo { get; set; }
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
    public class PpmLoginBadResponse:IPpmResponse
    {
        [XmlElement(ElementName = "Results")]
        public string Results { get; set; }
        [XmlElement(ElementName = "Error")]
        public Error Error { get; set; }
        [XmlAttribute(AttributeName = "s")]
        public string S { get; set; }
        [XmlAttribute(AttributeName = "lst")]
        public string Lst { get; set; }
    }

}
