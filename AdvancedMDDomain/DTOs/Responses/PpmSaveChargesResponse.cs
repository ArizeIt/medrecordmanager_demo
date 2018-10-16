using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Responses
{
    [XmlRoot(ElementName = "Results")]
    public class SaveChanrgesResults
    {
        [XmlAttribute(AttributeName = "success")]
        public string Success { get; set; }
    }

    [XmlRoot(ElementName = "PPMDResults")]
    public class PpmSaveChargesResponse :IPpmResponse
    {
        [XmlElement(ElementName = "Results")]
        public SaveChanrgesResults Results { get; set; }
        [XmlElement(ElementName = "Error")]
        public string Error { get; set; }
    }
   
}
