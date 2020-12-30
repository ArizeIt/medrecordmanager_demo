using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Requests
{
    [XmlRoot(ElementName = "appt")]
    public class Appt
    {
        [XmlAttribute(AttributeName = "patient")]
        public string PatientId { get; set; }
        [XmlAttribute(AttributeName = "profile")]
        public string ProfileId { get; set; }
        [XmlAttribute(AttributeName = "date")]
        public string Date { get; set; }
        [XmlAttribute(AttributeName = "time")]
        public string Time { get; set; }
        [XmlAttribute(AttributeName = "types")]
        public string Types { get; set; }
        [XmlAttribute(AttributeName = "duration")]
        public string Duration { get; set; }
        [XmlAttribute(AttributeName = "column")]
        public string Column { get; set; }
        [XmlAttribute(AttributeName = "force")]
        public string Force { get; set; }

    }

    [XmlRoot(ElementName = "ppmdmsg")]
    public class PpmAddVisitRequest
    {
        [XmlElement(ElementName = "appt")]
        public Appt Appt { get; set; }
        [XmlAttribute(AttributeName = "action")]
        public string Action { get; set; }
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
        [XmlAttribute(AttributeName = "msgtime")]
        public string Msgtime { get; set; }
    }
}
