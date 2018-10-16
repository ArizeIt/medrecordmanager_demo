using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Requests
{
    [XmlRoot(ElementName = "masterfile")]
    public class Masterfile
    {
        [XmlAttribute(AttributeName = "uid")]
        public string Uid { get; set; }
        [XmlAttribute(AttributeName = "patientfid")]
        public string Patientfid { get; set; }
        [XmlAttribute(AttributeName = "profilefid")]
        public string Profilefid { get; set; }
        [XmlAttribute(AttributeName = "notetypefid")]
        public string Notetypefid { get; set; }
        [XmlAttribute(AttributeName = "case_note")]
        public string Case_note { get; set; }
    }

    [XmlRoot(ElementName = "ppmdmsg")]
    public class PpmSavePatientNoteRequest : IPpmRequest
    {
        [XmlElement(ElementName = "masterfile")]
        public Masterfile Masterfile { get; set; }
        [XmlAttribute(AttributeName = "action")]
        public string Action { get; set; }
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
        [XmlAttribute(AttributeName = "msgtime")]
        public string Msgtime { get; set; }
        [XmlAttribute(AttributeName = "ltq")]
        public string Ltq { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "useclienttime")]
        public string Useclienttime { get; set; }
        [XmlAttribute(AttributeName = "la")]
        public string La { get; set; }
        [XmlAttribute(AttributeName = "lac")]
        public string Lac { get; set; }
        [XmlAttribute(AttributeName = "lat")]
        public string Lat { get; set; }
        [XmlAttribute(AttributeName = "let")]
        public string Let { get; set; }
        [XmlAttribute(AttributeName = "lst")]
        public string Lst { get; set; }
    }
}
