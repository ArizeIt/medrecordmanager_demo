using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Requests
{

    [XmlRoot(ElementName = "category")]
    public class RequestCategory
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "filegroupfid")]
        public string Filegroupfid { get; set; }
        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "filetype")]
        public string Filetype { get; set; }
        [XmlAttribute(AttributeName = "level")]
        public string Level { get; set; }
        [XmlAttribute(AttributeName = "default")]
        public string Default { get; set; }
    }

    [XmlRoot(ElementName = "categorylist")]
    public class RequestCategorylist
    {
        [XmlElement(ElementName = "category")]
        public RequestCategory Category { get; set; }
    }

    [XmlRoot(ElementName = "group")]
    public class Group
    {
        [XmlElement(ElementName = "categorylist")]
        public RequestCategorylist Categorylist { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "grouplist")]
    public class Grouplist
    {
        [XmlElement(ElementName = "group")]
        public Group Group { get; set; }
    }

    [XmlRoot(ElementName = "file")]
    public class RequestFile
    {
        [XmlElement(ElementName = "grouplist")]
        public Grouplist Grouplist { get; set; }
        [XmlElement(ElementName = "filecontents")]
        public string Filecontents { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "description")]
        public string Description { get; set; }
        [XmlAttribute(AttributeName = "filetype")]
        public string Filetype { get; set; }
        [XmlAttribute(AttributeName = "fileext")]
        public string Fileext { get; set; }
        [XmlAttribute(AttributeName = "visitid")]
        public string Visitid { get; set; }
        [XmlAttribute(AttributeName = "profileid")]
        public string Profileid { get; set; }
        [XmlAttribute(AttributeName = "facilityid")]
        public string Facilityid { get; set; }
        [XmlAttribute(AttributeName = "providerid")]
        public string Providerid { get; set; }
        [XmlAttribute(AttributeName = "dos")]
        public string Dos { get; set; }
        [XmlAttribute(AttributeName = "comments")]
        public string Comments { get; set; }
        [XmlAttribute(AttributeName = "patientid")]
        public string Patientid { get; set; }
        [XmlAttribute(AttributeName = "local_file")]
        public string Local_file { get; set; }
        [XmlAttribute(AttributeName = "referringproviderid")]
        public string Referringproviderid { get; set; }
        [XmlAttribute(AttributeName = "savechanges")]
        public string Savechanges { get; set; }
        [XmlAttribute(AttributeName = "zipmode")]
        public string Zipmode { get; set; }
    }

    [XmlRoot(ElementName = "ppmdmsg")]
    public class PpmUploadFileRequest : IPpmRequest
    {
        [XmlElement(ElementName = "file")]
        public RequestFile File { get; set; }
        [XmlAttribute(AttributeName = "action")]
        public string Action { get; set; }
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
        [XmlAttribute(AttributeName = "msgtime")]
        public string Msgtime { get; set; }
        [XmlAttribute(AttributeName = "ltq")]
        public string Ltq { get; set; }
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
