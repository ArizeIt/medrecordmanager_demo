using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Responses
{
    [XmlRoot(ElementName = "visit")]
    public class PpmVisit
    {
        [XmlElement(ElementName = "chargelist")]
        public string Chargelist { get; set; }
        [XmlElement(ElementName = "extins")]
        public string Extins { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "date")]
        public string Date { get; set; }
        [XmlAttribute(AttributeName = "isappt")]
        public string Isappt { get; set; }
        [XmlAttribute(AttributeName = "profile")]
        public string Profile { get; set; }
        [XmlAttribute(AttributeName = "episode")]
        public string Episode { get; set; }
        [XmlAttribute(AttributeName = "insorder")]
        public string Insorder { get; set; }
        [XmlAttribute(AttributeName = "note")]
        public string Note { get; set; }
        [XmlAttribute(AttributeName = "forcepaper")]
        public string Forcepaper { get; set; }
    }

    [XmlRoot(ElementName = "Results")]
    public class VisitResults
    {
        [XmlElement(ElementName = "visit")]
        public PpmVisit Visit { get; set; }
        [XmlElement(ElementName = "notelist")]
        public string Notelist { get; set; }
    }

    [XmlRoot(ElementName = "PPMDResults")]
    public class PpmAddVisitResponse :IPpmResponse
    {
        [XmlElement(ElementName = "Results")]
        public VisitResults Results { get; set; }
        [XmlElement(ElementName = "Error")]
        public string Error { get; set; }
    }

}
