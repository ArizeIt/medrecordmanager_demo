using System.Collections.Generic;
using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Responses
{
    [XmlRoot(ElementName = "address")]
    public class Address
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
    }

    [XmlRoot(ElementName = "contactinfo")]
    public class Contactinfo
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
    }

    [XmlRoot(ElementName = "arbucket")]
    public class Arbucket
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
    public class Arbucketlist
    {
        [XmlElement(ElementName = "arbucket")]
        public List<Arbucket> Arbucket { get; set; }
    }

    [XmlRoot(ElementName = "patient")]
    public class Patient
    {
        [XmlElement(ElementName = "address")]
        public Address Address { get; set; }
        [XmlElement(ElementName = "contactinfo")]
        public Contactinfo Contactinfo { get; set; }
        [XmlElement(ElementName = "arbucketlist")]
        public Arbucketlist Arbucketlist { get; set; }
        [XmlAttribute(AttributeName = "dob")]
        public string Dob { get; set; }
        [XmlAttribute(AttributeName = "deceased")]
        public string Deceased { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "respparty")]
        public string Respparty { get; set; }
        [XmlAttribute(AttributeName = "profile")]
        public string Profile { get; set; }
        [XmlAttribute(AttributeName = "insorder")]
        public string Insorder { get; set; }
        [XmlAttribute(AttributeName = "sex")]
        public string Sex { get; set; }
        [XmlAttribute(AttributeName = "chart")]
        public string Chart { get; set; }
        [XmlAttribute(AttributeName = "maritalstatus")]
        public string Maritalstatus { get; set; }
        [XmlAttribute(AttributeName = "employerid")]
        public string Employerid { get; set; }
        [XmlAttribute(AttributeName = "employer")]
        public string Employer { get; set; }
        [XmlAttribute(AttributeName = "ssn")]
        public string Ssn { get; set; }
        [XmlAttribute(AttributeName = "finclasscode")]
        public string Finclasscode { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "title")]
        public string Title { get; set; }
        [XmlAttribute(AttributeName = "relationship")]
        public string Relationship { get; set; }
        [XmlAttribute(AttributeName = "hipaarelationship")]
        public string Hipaarelationship { get; set; }
    }

    [XmlRoot(ElementName = "patientlist")]
    public class Patientlist
    {
        [XmlElement(ElementName = "patient")]
        public Patient Patient { get; set; }
    }

    [XmlRoot(ElementName = "familymember")]
    public class Familymember
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
    public class Familymemberlist
    {
        [XmlElement(ElementName = "familymember")]
        public Familymember Familymember { get; set; }
    }

    [XmlRoot(ElementName = "respparty")]
    public class Respparty
    {
        [XmlElement(ElementName = "address")]
        public Address Address { get; set; }
        [XmlElement(ElementName = "contactinfo")]
        public Contactinfo Contactinfo { get; set; }
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
    }

    [XmlRoot(ElementName = "resppartylist")]
    public class Resppartylist
    {
        [XmlElement(ElementName = "respparty")]
        public Respparty Respparty { get; set; }
    }

    [XmlRoot(ElementName = "Results")]
    public class Results
    {
        [XmlElement(ElementName = "patientlist")]
        public Patientlist Patientlist { get; set; }
        [XmlElement(ElementName = "resppartylist")]
        public Resppartylist Resppartylist { get; set; }
        [XmlAttribute(AttributeName = "success")]
        public string Success { get; set; }
    }

    [XmlRoot(ElementName = "PPMDResults")]
    public class PpmAddPatientResponse : IPpmResponse
    {
        [XmlElement(ElementName = "Results")]
        public Results Results { get; set; }
        [XmlElement(ElementName = "Error")]
        public string Error { get; set; }
    }

}
