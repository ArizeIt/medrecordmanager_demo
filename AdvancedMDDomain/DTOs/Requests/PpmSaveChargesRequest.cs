using System.Collections.Generic;
using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Requests
{
    [XmlRoot(ElementName = "charge")]
    public class Charge
    {
        [XmlAttribute(AttributeName = "proccode")]
        public string Proccode { get; set; }
        [XmlAttribute(AttributeName = "units")]
        public string Units { get; set; }
        [XmlAttribute(AttributeName = "diagcodes")]
        public string Diagcodes { get; set; }
        [XmlAttribute(AttributeName = "codeset")]
        public string Codeset { get; set; }
        [XmlAttribute(AttributeName = "modcodes")]
        public string Modcodes { get; set; }
    }

    [XmlRoot(ElementName = "chargelist")]
    public class Chargelist
    {
        [XmlElement(ElementName = "charge")]
        public List<Charge> Charge { get; set; }
    }

    [XmlRoot(ElementName = "visit")]
    public class AmdVisit
    {
        [XmlElement(ElementName = "chargelist")]
        public Chargelist Chargelist { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "force")]
        public string Force { get; set; }
    }

    [XmlRoot(ElementName = "ppmdmsg")]
    public class PpmSaveChargesRequest : IPpmRequest
    {
        [XmlElement(ElementName = "visit")]
        public AmdVisit Visit { get; set; }
        [XmlAttribute(AttributeName = "action")]
        public string Action { get; set; }
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
        [XmlAttribute(AttributeName = "msgtime")]
        public string Msgtime { get; set; }
        [XmlAttribute(AttributeName = "patientid")]
        public string Patientid { get; set; }
    }

}
