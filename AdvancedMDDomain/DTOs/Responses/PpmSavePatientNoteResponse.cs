using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Responses
{
    [XmlRoot(ElementName = "note")]
    public class Note
    {
        [XmlAttribute(AttributeName = "space", Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string Space { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "record")]
    public class Record
    {
        [XmlElement(ElementName = "note")]
        public Note Note { get; set; }
        [XmlAttribute(AttributeName = "uid")]
        public string Uid { get; set; }
        [XmlAttribute(AttributeName = "profilefid")]
        public string Profilefid { get; set; }
        [XmlAttribute(AttributeName = "profilecode")]
        public string Profilecode { get; set; }
        [XmlAttribute(AttributeName = "profilename")]
        public string Profilename { get; set; }
        [XmlAttribute(AttributeName = "notetypefid")]
        public string Notetypefid { get; set; }
        [XmlAttribute(AttributeName = "notetypecode")]
        public string Notetypecode { get; set; }
        [XmlAttribute(AttributeName = "createdat")]
        public string Createdat { get; set; }
        [XmlAttribute(AttributeName = "createuser")]
        public string Createuser { get; set; }
        [XmlAttribute(AttributeName = "changedat")]
        public string Changedat { get; set; }
    }

    [XmlRoot(ElementName = "PPMDResults")]
    public class PpmSavePatientNoteResponse : IPpmResponse
    {
        [XmlElement(ElementName = "record")]
        public Record Record { get; set; }
        [XmlAttribute(AttributeName = "s")]
        public string S { get; set; }
        [XmlAttribute(AttributeName = "lst")]
        public string Lst { get; set; }
        [XmlAttribute(AttributeName = "newid")]
        public string Newid { get; set; }
    }

}
