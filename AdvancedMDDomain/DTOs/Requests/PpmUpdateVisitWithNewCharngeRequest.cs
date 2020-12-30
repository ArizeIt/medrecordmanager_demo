using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Requests
{
    [XmlRoot(ElementName = "charge")]
    public class UpdateCharge
    {
        [XmlAttribute(AttributeName = "respparty")]
        public string Respparty { get; set; }
        [XmlAttribute(AttributeName = "begindate")]
        public string Begindate { get; set; }
        [XmlAttribute(AttributeName = "enddate")]
        public string Enddate { get; set; }
        [XmlAttribute(AttributeName = "diagcodes")]
        public string Diagcodes { get; set; }
        [XmlAttribute(AttributeName = "modcodes")]
        public string Modcodes { get; set; }
        [XmlAttribute(AttributeName = "finclasscode")]
        public string Finclasscode { get; set; }
        [XmlAttribute(AttributeName = "aging")]
        public string Aging { get; set; }
        [XmlAttribute(AttributeName = "billins")]
        public string Billins { get; set; }
        [XmlAttribute(AttributeName = "proccode")]
        public string Proccode { get; set; }
        [XmlAttribute(AttributeName = "cobcode")]
        public string Cobcode { get; set; }
        [XmlAttribute(AttributeName = "pos")]
        public string Pos { get; set; }
        [XmlAttribute(AttributeName = "tos")]
        public string Tos { get; set; }
        [XmlAttribute(AttributeName = "units")]
        public string Units { get; set; }
        [XmlAttribute(AttributeName = "netfee")]
        public string Netfee { get; set; }
        [XmlAttribute(AttributeName = "fee")]
        public string Fee { get; set; }
        [XmlAttribute(AttributeName = "allowed")]
        public string Allowed { get; set; }
        [XmlAttribute(AttributeName = "patportion")]
        public string Patportion { get; set; }
        [XmlAttribute(AttributeName = "insportion")]
        public string Insportion { get; set; }
        [XmlAttribute(AttributeName = "patbalance")]
        public string Patbalance { get; set; }
        [XmlAttribute(AttributeName = "insbalance")]
        public string Insbalance { get; set; }
        [XmlAttribute(AttributeName = "batchnumber")]
        public string BatchNumber { get; set; }

    }

    [XmlRoot(ElementName = "chargelist")]
    public class UpdateChargelist
    {
        [XmlElement(ElementName = "charge")]
        public List<UpdateCharge> Charges { get; set; }
    }

    [XmlRoot(ElementName = "visit")]
    public class UpdateVisit
    {
        [XmlElement(ElementName = "chargelist")]
        public UpdateChargelist Chargelist { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "date")]
        public string Date { get; set; }
        [XmlAttribute(AttributeName = "profile")]
        public string Profile { get; set; }
        [XmlAttribute(AttributeName = "insorder")]
        public string Insorder { get; set; }
        [XmlAttribute(AttributeName = "refplan")]
        public string Refplan { get; set; }
        [XmlAttribute(AttributeName = "facility")]
        public string Facility { get; set; }
        [XmlAttribute(AttributeName = "force")]
        public string Force { get; set; }
    }

    [XmlRoot(ElementName = "ppmdmsg")]
    public class PpmUpdateVisitWithNewCharngeRequest : IPpmRequest
    {
        [XmlElement(ElementName = "visit")]
        public UpdateVisit Visit { get; set; }
        [XmlAttribute(AttributeName = "action")]
        public string Action { get; set; }
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
        [XmlAttribute(AttributeName = "msgtime")]
        public string Msgtime { get; set; }
        [XmlAttribute(AttributeName = "patientid")]
        public string Patientid { get; set; }
        [XmlAttribute(AttributeName = "episodeid")]
        public string Episodeid { get; set; }
        [XmlAttribute(AttributeName = "approval")]
        public string Approval { get; set; }
    }

}
