using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Responses
{
    [XmlRoot(ElementName = "diagcode")]
    public class Diagcode
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "diagcodelist")]
    public class Diagcodelist
    {
        [XmlElement(ElementName = "diagcode")]
        public Diagcode Diagcode { get; set; }
    }

    [XmlRoot(ElementName = "Results")]
    public class LookupDiagCodeResults
    {
        [XmlElement(ElementName = "diagcodelist")]
        public Diagcodelist Diagcodelist { get; set; }
    }

    [XmlRoot(ElementName = "PPMDResults")]
    public class PpmLookupDiagCodeResponse: IPpmResponse
    {
        [XmlElement(ElementName = "Results")]
        public LookupDiagCodeResults Results { get; set; }
        [XmlAttribute(AttributeName = "s")]
        public string S { get; set; }
        [XmlAttribute(AttributeName = "lst")]
        public string Lst { get; set; }
    }
}
