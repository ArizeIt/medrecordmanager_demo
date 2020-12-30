using System.Collections.Generic;
using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Responses
{
    [XmlRoot(ElementName = "profile")]
    public class Profile
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "chargefeesched")]
        public string Chargefeesched { get; set; }
        [XmlAttribute(AttributeName = "fullinactive")]
        public string Fullinactive { get; set; }
        [XmlAttribute(AttributeName = "status")]
        public string Status { get; set; }
        [XmlAttribute(AttributeName = "codesetpreference")]
        public string Codesetpreference { get; set; }
        [XmlAttribute(AttributeName = "email")]
        public string Email { get; set; }
        [XmlAttribute(AttributeName = "isinstitutional")]
        public string Isinstitutional { get; set; }
    }

    [XmlRoot(ElementName = "profilelist")]
    public class Profilelist
    {
        [XmlElement(ElementName = "profile")]
        public List<Profile> Profile { get; set; }
    }

    [XmlRoot(ElementName = "Results")]
    public class PpmLookUpProviderResults
    {
        [XmlElement(ElementName = "profilelist")]
        public Profilelist Profilelist { get; set; }
    }

    [XmlRoot(ElementName = "PPMDResults")]
    public class PpmLookUpProviderResponse : IPpmResponse
    {
        [XmlElement(ElementName = "Results")]
        public PpmLookUpProviderResults Results { get; set; }
        [XmlElement(ElementName = "Error")]
        public string Error { get; set; }
        [XmlAttribute(AttributeName = "s")]
        public string S { get; set; }
        [XmlAttribute(AttributeName = "lst")]
        public string Lst { get; set; }

    }

}
