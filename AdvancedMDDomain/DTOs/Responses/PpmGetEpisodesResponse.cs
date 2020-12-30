using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Responses
{
    [XmlRoot(ElementName = "episode")]
    public class Episode
    {
        [XmlElement(ElementName = "extins")]
        public string Extins { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "insorder")]
        public string Insorder { get; set; }
        [XmlAttribute(AttributeName = "respparty")]
        public string Respparty { get; set; }
        [XmlAttribute(AttributeName = "finclasscode")]
        public string Finclasscode { get; set; }
        [XmlAttribute(AttributeName = "refplan")]
        public string Refplan { get; set; }
        [XmlAttribute(AttributeName = "groupbymonth")]
        public string Groupbymonth { get; set; }
        [XmlAttribute(AttributeName = "plancreatedate")]
        public string Plancreatedate { get; set; }
        [XmlAttribute(AttributeName = "ptoffsetdays")]
        public string Ptoffsetdays { get; set; }
        [XmlAttribute(AttributeName = "stoffsetdays")]
        public string Stoffsetdays { get; set; }
        [XmlAttribute(AttributeName = "otoffsetdays")]
        public string Otoffsetdays { get; set; }
        [XmlAttribute(AttributeName = "maintenancedate")]
        public string Maintenancedate { get; set; }
    }

    [XmlRoot(ElementName = "episodelist")]
    public class Episodelist
    {
        [XmlElement(ElementName = "episode")]
        public Episode Episode { get; set; }
    }

    [XmlRoot(ElementName = "refplan")]
    public class Refplan
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "refprov")]
        public string Refprov { get; set; }
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlAttribute(AttributeName = "reason")]
        public string Reason { get; set; }
        [XmlAttribute(AttributeName = "preauthreq")]
        public string Preauthreq { get; set; }
        [XmlAttribute(AttributeName = "preauth")]
        public string Preauth { get; set; }
        [XmlAttribute(AttributeName = "begindate")]
        public string Begindate { get; set; }
        [XmlAttribute(AttributeName = "enddate")]
        public string Enddate { get; set; }
        [XmlAttribute(AttributeName = "maxvisits")]
        public string Maxvisits { get; set; }
        [XmlAttribute(AttributeName = "usedvisits")]
        public string Usedvisits { get; set; }
        [XmlAttribute(AttributeName = "maxamount")]
        public string Maxamount { get; set; }
        [XmlAttribute(AttributeName = "usedamount")]
        public string Usedamount { get; set; }
        [XmlAttribute(AttributeName = "sequence")]
        public string Sequence { get; set; }
        [XmlAttribute(AttributeName = "ismain")]
        public string Ismain { get; set; }
    }

    [XmlRoot(ElementName = "refplanlist")]
    public class Refplanlist
    {
        [XmlElement(ElementName = "refplan")]
        public Refplan Refplan { get; set; }
    }

    [XmlRoot(ElementName = "insplan")]
    public class GetEpisodesInsplan
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "carrier")]
        public string Carrier { get; set; }
        [XmlAttribute(AttributeName = "subscriber")]
        public string Subscriber { get; set; }
        [XmlAttribute(AttributeName = "sequence")]
        public string Sequence { get; set; }
    }

    [XmlRoot(ElementName = "insplanlist")]
    public class GetEpisodesInsplanlist
    {
        [XmlElement(ElementName = "insplan")]
        public GetEpisodesInsplan Insplan { get; set; }
    }

    [XmlRoot(ElementName = "patient")]
    public class GetEpisodesPatient
    {
        [XmlElement(ElementName = "episodelist")]
        public Episodelist Episodelist { get; set; }
        [XmlElement(ElementName = "refplanlist")]
        public Refplanlist Refplanlist { get; set; }
        [XmlElement(ElementName = "insplanlist")]
        public GetEpisodesInsplanlist Insplanlist { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "respparty")]
        public string Respparty { get; set; }
        [XmlAttribute(AttributeName = "profile")]
        public string Profile { get; set; }
        [XmlAttribute(AttributeName = "insorder")]
        public string Insorder { get; set; }
        [XmlAttribute(AttributeName = "chart")]
        public string Chart { get; set; }
        [XmlAttribute(AttributeName = "finclasscode")]
        public string Finclasscode { get; set; }
    }

    [XmlRoot(ElementName = "patientlist")]
    public class GetEpisodesPatientlist
    {
        [XmlElement(ElementName = "patient")]
        public GetEpisodesPatient Patient { get; set; }
    }

    [XmlRoot(ElementName = "carrier")]
    public class GetEpisodesCarrier
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }
    }

    [XmlRoot(ElementName = "carrierlist")]
    public class GetEpisodesCarrierlist
    {
        [XmlElement(ElementName = "carrier")]
        public GetEpisodesCarrier Carrier { get; set; }
    }

    [XmlRoot(ElementName = "respparty")]
    public class GetEpisodesRespparty
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "resppartylist")]
    public class GetEpisodesResppartylist
    {
        [XmlElement(ElementName = "respparty")]
        public Requests.Respparty Respparty { get; set; }
    }

    [XmlRoot(ElementName = "finclass")]
    public class GetEpisodesFinclass
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "finclasslist")]
    public class GetEpisodesFinclasslist
    {
        [XmlElement(ElementName = "finclass")]
        public GetEpisodesFinclass Finclass { get; set; }
    }

    [XmlRoot(ElementName = "refprovider")]
    public class Refprovider
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "refproviderlist")]
    public class Refproviderlist
    {
        [XmlElement(ElementName = "refprovider")]
        public Refprovider Refprovider { get; set; }
    }

    [XmlRoot(ElementName = "Results")]
    public class GetEpisodesResults
    {
        [XmlElement(ElementName = "patientlist")]
        public GetEpisodesPatientlist Patientlist { get; set; }
        [XmlElement(ElementName = "carrierlist")]
        public GetEpisodesCarrierlist Carrierlist { get; set; }
        [XmlElement(ElementName = "resppartylist")]
        public GetEpisodesResppartylist Resppartylist { get; set; }
        [XmlElement(ElementName = "finclasslist")]
        public GetEpisodesFinclasslist Finclasslist { get; set; }
        [XmlElement(ElementName = "refproviderlist")]
        public Refproviderlist Refproviderlist { get; set; }
    }

    [XmlRoot(ElementName = "PPMDResults")]
    public class PpmGetEpisodesResponse : IPpmResponse
    {
        [XmlElement(ElementName = "Results")]
        public GetEpisodesResults Results { get; set; }
        [XmlAttribute(AttributeName = "s")]
        public string S { get; set; }
        [XmlAttribute(AttributeName = "lst")]
        public string Lst { get; set; }
        [XmlAttribute(AttributeName = "n")]
        public string N { get; set; }

        [XmlIgnore]
        public string Error { get; set; }
    }

}
