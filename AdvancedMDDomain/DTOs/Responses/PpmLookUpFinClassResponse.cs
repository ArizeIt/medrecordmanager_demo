using System.Collections.Generic;
using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Responses
{

    [XmlRoot(ElementName = "finclass")]
    public class Finclass
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "billins")]
        public string Billins { get; set; }
        [XmlAttribute(AttributeName = "billlab")]
        public string Billlab { get; set; }
        [XmlAttribute(AttributeName = "acceptassign")]
        public string Acceptassign { get; set; }
        [XmlAttribute(AttributeName = "responsible")]
        public string Responsible { get; set; }
        [XmlAttribute(AttributeName = "allowfeesched")]
        public string Allowfeesched { get; set; }
    }

    [XmlRoot(ElementName = "finclasslist")]
    public class Finclasslist
    {
        [XmlElement(ElementName = "finclass")]
        public List<Finclass> Finclass { get; set; }
    }

    [XmlRoot(ElementName = "feesched")]
    public class Feesched
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
    }

    [XmlRoot(ElementName = "feeschedlist")]
    public class Feeschedlist
    {
        [XmlElement(ElementName = "feesched")]
        public Feesched Feesched { get; set; }
    }

    [XmlRoot(ElementName = "Results")]
    public class FinClassResults
    {
        [XmlElement(ElementName = "finclasslist")]
        public Finclasslist Finclasslist { get; set; }
        [XmlElement(ElementName = "feeschedlist")]
        public Feeschedlist Feeschedlist { get; set; }
    }

    [XmlRoot(ElementName = "PPMDResults")]
    public class PpmLookUpFinClassResponse : IPpmResponse
    {
        [XmlElement(ElementName = "Results")]
        public FinClassResults Results { get; set; }
        [XmlElement(ElementName = "Error")]
        public string Error { get; set; }
        [XmlAttribute(AttributeName = "s")]
        public string S { get; set; }
        [XmlAttribute(AttributeName = "lst")]
        public string Lst { get; set; }
    }

}
