using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Requests
{
    [XmlRoot(ElementName = "ppmdmsg")]
    public class PpmLoginRequest
    {
        [XmlAttribute(AttributeName = "action")]
        public string Action { get; set; }

        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }

        [XmlAttribute(AttributeName = "msgtime")]
        public string Msgtime { get; set; }

        [XmlAttribute(AttributeName = "nocookie")]
        public int NoCooki { get; set; } 

        [XmlAttribute(AttributeName = "username")]
        public string Username { get; set; }

        [XmlAttribute(AttributeName = "psw")]
        public string Password { get; set; }

        [XmlAttribute(AttributeName = "officecode")]
        public string Officecode { get; set; }

        [XmlAttribute(AttributeName = "appname")]
        public string Appname { get; set; }
    }

}
