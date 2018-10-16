using System.Collections.Generic;
using System.Xml.Serialization;
using AdvancedMDDomain;

[XmlRoot(ElementName = "address")]
public class LookUpAddress
{
    [XmlAttribute(AttributeName = "areacode")]
    public string Areacode { get; set; }
    [XmlAttribute(AttributeName = "countrycode")]
    public string Countrycode { get; set; }
    [XmlAttribute(AttributeName = "address2")]
    public string Address2 { get; set; }
    [XmlAttribute(AttributeName = "address1")]
    public string Address1 { get; set; }
    [XmlAttribute(AttributeName = "state")]
    public string State { get; set; }
    [XmlAttribute(AttributeName = "city")]
    public string City { get; set; }
    [XmlAttribute(AttributeName = "zip")]
    public string Zip { get; set; }
    [XmlAttribute(AttributeName = "zipid")]
    public string Zipid { get; set; }
}

[XmlRoot(ElementName = "contactinfo")]
public class LookUpContactinfo
{
    [XmlAttribute(AttributeName = "homephone")]
    public string Homephone { get; set; }
}

[XmlRoot(ElementName = "patient")]
public class LookUpPatient
{
    [XmlElement(ElementName = "address")]
    public LookUpAddress Address { get; set; }
    [XmlElement(ElementName = "contactinfo")]
    public LookUpContactinfo Contactinfo { get; set; }
    [XmlAttribute(AttributeName = "dob")]
    public string Dob { get; set; }
    [XmlAttribute(AttributeName = "ssn")]
    public string Ssn { get; set; }
    [XmlAttribute(AttributeName = "chart")]
    public string Chart { get; set; }
    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; }
    [XmlAttribute(AttributeName = "id")]
    public string Id { get; set; }
}

[XmlRoot(ElementName = "patientlist")]
public class LookUpPatientlist
{
    [XmlElement(ElementName = "patient")]
    public List<LookUpPatient> Patients { get; set; }
    [XmlAttribute(AttributeName = "itemsto")]
    public string Itemsto { get; set; }
    [XmlAttribute(AttributeName = "itemsfrom")]
    public string Itemsfrom { get; set; }
    [XmlAttribute(AttributeName = "pagecount")]
    public string Pagecount { get; set; }
    [XmlAttribute(AttributeName = "page")]
    public string Page { get; set; }
    [XmlAttribute(AttributeName = "itemcount")]
    public string Itemcount { get; set; }
}

[XmlRoot(ElementName = "Results")]
public class LookUpPatientResults
{
    [XmlElement(ElementName = "patientlist")]
    public LookUpPatientlist Patientlist { get; set; }
}

[XmlRoot(ElementName = "PPMDResults")]
public class PpmLookUpPatientResponse: IPpmResponse
{
    [XmlElement(ElementName = "Results")]
    public LookUpPatientResults Results { get; set; }
    [XmlElement(ElementName = "Error")]
    public string Error { get; set; }
    [XmlAttribute(AttributeName = "lst")]
    public string Lst { get; set; }
    [XmlAttribute(AttributeName = "s")]
    public string S { get; set; }
}