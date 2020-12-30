using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Requests
{
    [XmlRoot(ElementName = "insplan")]
    public class Insplan
    {
        [XmlElement(ElementName = "insnote")]
        public string Insnote { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "begindate")]
        public string Begindate { get; set; }
        [XmlAttribute(AttributeName = "enddate")]
        public string Enddate { get; set; }
        [XmlAttribute(AttributeName = "carrier")]
        public string Carrier { get; set; }
        [XmlAttribute(AttributeName = "subscriber")]
        public string Subscriber { get; set; }
        [XmlAttribute(AttributeName = "subscribernum")]
        public string Subscribernum { get; set; }
        [XmlAttribute(AttributeName = "hipaarelationship")]
        public string Hipaarelationship { get; set; }
        [XmlAttribute(AttributeName = "relationship")]
        public string Relationship { get; set; }
        [XmlAttribute(AttributeName = "grpname")]
        public string Grpname { get; set; }
        [XmlAttribute(AttributeName = "grpnum")]
        public string Grpnum { get; set; }
        [XmlAttribute(AttributeName = "copay")]
        public string Copay { get; set; }
        [XmlAttribute(AttributeName = "copaytype")]
        public string Copaytype { get; set; }
        [XmlAttribute(AttributeName = "coverage")]
        public string Coverage { get; set; }
        [XmlAttribute(AttributeName = "payerid")]
        public string Payerid { get; set; }
        [XmlAttribute(AttributeName = "mspcode")]
        public string Mspcode { get; set; }
        [XmlAttribute(AttributeName = "eligibilityid")]
        public string Eligibilityid { get; set; }
        [XmlAttribute(AttributeName = "eligibilitystatusid")]
        public string Eligibilitystatusid { get; set; }
        [XmlAttribute(AttributeName = "eligibilitychangedat")]
        public string Eligibilitychangedat { get; set; }
        [XmlAttribute(AttributeName = "eligibilitycreatedat")]
        public string Eligibilitycreatedat { get; set; }
        [XmlAttribute(AttributeName = "eligibilityresponsedate")]
        public string Eligibilityresponsedate { get; set; }
        [XmlAttribute(AttributeName = "deductible")]
        public string Deductible { get; set; }
        [XmlAttribute(AttributeName = "deductiblemet")]
        public string Deductiblemet { get; set; }
        [XmlAttribute(AttributeName = "yearendmonth")]
        public string Yearendmonth { get; set; }
        [XmlAttribute(AttributeName = "lifetime")]
        public string Lifetime { get; set; }
    }

    [XmlRoot(ElementName = "insplanlist")]
    public class InsuranceInsplanlist
    {
        [XmlElement(ElementName = "insplan")]
        public Insplan Insplan { get; set; }
    }

    [XmlRoot(ElementName = "patient")]
    public class InsRequestPatient
    {
        [XmlElement(ElementName = "insplanlist")]
        public InsuranceInsplanlist Insplanlist { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "changed")]
        public string Changed { get; set; }
    }

    [XmlRoot(ElementName = "ppmdmsg")]
    public class PpmAddInsuranceRequest : IPpmRequest
    {
        [XmlElement(ElementName = "patient")]
        public InsRequestPatient Patient { get; set; }
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
