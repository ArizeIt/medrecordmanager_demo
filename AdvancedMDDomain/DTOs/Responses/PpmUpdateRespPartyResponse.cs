using System.Collections.Generic;
using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Responses
{
    [XmlRoot(ElementName = "address")]
    public class UpdateRespPartyAddress
    {
        [XmlAttribute(AttributeName = "zip")]
        public string Zip { get; set; }
        [XmlAttribute(AttributeName = "city")]
        public string City { get; set; }
        [XmlAttribute(AttributeName = "state")]
        public string State { get; set; }
        [XmlAttribute(AttributeName = "address1")]
        public string Address1 { get; set; }
        [XmlAttribute(AttributeName = "address2")]
        public string Address2 { get; set; }
        [XmlAttribute(AttributeName = "areacode")]
        public string Areacode { get; set; }
        [XmlAttribute(AttributeName = "countrycode")]
        public string Countrycode { get; set; }
    }

    [XmlRoot(ElementName = "contactinfo")]
    public class UpdateRespPartyContactinfo
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "homephone")]
        public string Homephone { get; set; }
        [XmlAttribute(AttributeName = "officephone")]
        public string Officephone { get; set; }
        [XmlAttribute(AttributeName = "officeext")]
        public string Officeext { get; set; }
        [XmlAttribute(AttributeName = "otherphone")]
        public string Otherphone { get; set; }
        [XmlAttribute(AttributeName = "othertype")]
        public string Othertype { get; set; }
        [XmlAttribute(AttributeName = "email")]
        public string Email { get; set; }
        [XmlAttribute(AttributeName = "preferredcommunicationfid")]
        public string Preferredcommunicationfid { get; set; }
        [XmlAttribute(AttributeName = "communicationnote")]
        public string Communicationnote { get; set; }
    }

    [XmlRoot(ElementName = "arbucket")]
    public class UpdateRespPartyArbucket
    {
        [XmlAttribute(AttributeName = "buckettype")]
        public string Buckettype { get; set; }
        [XmlAttribute(AttributeName = "current")]
        public string Current { get; set; }
        [XmlAttribute(AttributeName = "past30")]
        public string Past30 { get; set; }
        [XmlAttribute(AttributeName = "past60")]
        public string Past60 { get; set; }
        [XmlAttribute(AttributeName = "past90")]
        public string Past90 { get; set; }
        [XmlAttribute(AttributeName = "past120")]
        public string Past120 { get; set; }
        [XmlAttribute(AttributeName = "unapplied")]
        public string Unapplied { get; set; }
        [XmlAttribute(AttributeName = "total")]
        public string Total { get; set; }
    }

    [XmlRoot(ElementName = "arbucketlist")]
    public class UpdateRespPartyArbucketlist
    {
        [XmlElement(ElementName = "arbucket")]
        public List<Arbucket> Arbucket { get; set; }
    }

    [XmlRoot(ElementName = "familymember")]
    public class UpdateRespPartyFamilymember
    {
        [XmlElement(ElementName = "contactinfo")]
        public Contactinfo Contactinfo { get; set; }
        [XmlElement(ElementName = "address")]
        public Address Address { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "chart")]
        public string Chart { get; set; }
        [XmlAttribute(AttributeName = "isrp")]
        public string Isrp { get; set; }
    }

    [XmlRoot(ElementName = "familymemberlist")]
    public class UpdateRespPartyFamilymemberlist
    {
        [XmlElement(ElementName = "familymember")]
        public Familymember Familymember { get; set; }
    }

    [XmlRoot(ElementName = "respparty")]
    public class UpdateRespPartyRespparty
    {
        [XmlElement(ElementName = "address")]
        public Address Address { get; set; }
        [XmlElement(ElementName = "contactinfo")]
        public Contactinfo Contactinfo { get; set; }
        [XmlElement(ElementName = "creditcardonfile")]
        public string Creditcardonfile { get; set; }
        [XmlElement(ElementName = "arbucketlist")]
        public Arbucketlist Arbucketlist { get; set; }
        [XmlElement(ElementName = "familymemberlist")]
        public Familymemberlist Familymemberlist { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "accttype")]
        public string Accttype { get; set; }
        [XmlAttribute(AttributeName = "acctnum")]
        public string Acctnum { get; set; }
        [XmlAttribute(AttributeName = "dob")]
        public string Dob { get; set; }
        [XmlAttribute(AttributeName = "sex")]
        public string Sex { get; set; }
        [XmlAttribute(AttributeName = "title")]
        public string Title { get; set; }
        [XmlAttribute(AttributeName = "employer")]
        public string Employer { get; set; }
        [XmlAttribute(AttributeName = "employstatus")]
        public string Employstatus { get; set; }
        [XmlAttribute(AttributeName = "ssn")]
        public string Ssn { get; set; }
        [XmlAttribute(AttributeName = "sendstmt")]
        public string Sendstmt { get; set; }
        [XmlAttribute(AttributeName = "stmtrestart")]
        public string Stmtrestart { get; set; }
        [XmlAttribute(AttributeName = "holdreasonfid")]
        public string Holdreasonfid { get; set; }
        [XmlAttribute(AttributeName = "holdreasondesc")]
        public string Holdreasondesc { get; set; }
        [XmlAttribute(AttributeName = "holdreasoncode")]
        public string Holdreasoncode { get; set; }
        [XmlAttribute(AttributeName = "billcycle")]
        public string Billcycle { get; set; }
        [XmlAttribute(AttributeName = "fincharge")]
        public string Fincharge { get; set; }
        [XmlAttribute(AttributeName = "stmtformat")]
        public string Stmtformat { get; set; }
        [XmlAttribute(AttributeName = "balancefwd")]
        public string Balancefwd { get; set; }
        [XmlAttribute(AttributeName = "lastpmtdate")]
        public string Lastpmtdate { get; set; }
        [XmlAttribute(AttributeName = "lastpmtamount")]
        public string Lastpmtamount { get; set; }
        [XmlAttribute(AttributeName = "laststmtdate")]
        public string Laststmtdate { get; set; }
        [XmlAttribute(AttributeName = "laststmtamount")]
        public string Laststmtamount { get; set; }
        [XmlAttribute(AttributeName = "paymentplanbalance")]
        public string Paymentplanbalance { get; set; }
    }

    [XmlRoot(ElementName = "resppartylist")]
    public class UpdateRespPartyResppartylist
    {
        [XmlElement(ElementName = "respparty")]
        public Respparty Respparty { get; set; }
    }

    [XmlRoot(ElementName = "accttype")]
    public class UpdateRespPartyAccttype
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }
    }

    [XmlRoot(ElementName = "accttypelist")]
    public class UpdateRespPartyAccttypelist
    {
        [XmlElement(ElementName = "accttype")]
        public Accttype Accttype { get; set; }
    }

    [XmlRoot(ElementName = "Results")]
    public class UpdateRespPartyResults
    {
        [XmlElement(ElementName = "resppartylist")]
        public Resppartylist Resppartylist { get; set; }
        [XmlElement(ElementName = "accttypelist")]
        public Accttypelist Accttypelist { get; set; }
    }

    [XmlRoot(ElementName = "PPMDResults")]
    public class PpmUpdateRespPartyResponse : IPpmResponse
    {
        [XmlElement(ElementName = "Results")]
        public UpdateRespPartyResults Results { get; set; }
        [XmlElement(ElementName = "Error")]
        public string Error { get; set; }
        [XmlAttribute(AttributeName = "s")]
        public string S { get; set; }
        [XmlAttribute(AttributeName = "lst")]
        public string Lst { get; set; }
        [XmlAttribute(AttributeName = "n")]
        public string N { get; set; }
    }
}
