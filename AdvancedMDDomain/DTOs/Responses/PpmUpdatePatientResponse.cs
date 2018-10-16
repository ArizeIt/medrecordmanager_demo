using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Responses
{
    [XmlRoot(ElementName = "Results")]
    public class UpdatePatientResults
    {
        [XmlElement(ElementName = "patientlist")]
        public Patientlist Patientlist { get; set; }
        [XmlElement(ElementName = "resppartylist")]
        public Resppartylist Resppartylist { get; set; }
        [XmlAttribute(AttributeName = "success")]
        public string Success { get; set; }
    }

    [XmlRoot(ElementName = "PPMDResults")]
    public class PpmUpdatePatientResponse:IPpmResponse
    {
        [XmlElement(ElementName = "Results")]
        public UpdatePatientResults Results { get; set; }
        [XmlElement(ElementName = "Error")]
        public string Error { get; set; }
    }
   
}
