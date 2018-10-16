using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Responses
{
    [XmlRoot(ElementName = "accttype")]
    public class Accttype
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "sendstmt")]
        public string Sendstmt { get; set; }
        [XmlAttribute(AttributeName = "balancefwd")]
        public string Balancefwd { get; set; }
        [XmlAttribute(AttributeName = "stmtrestart")]
        public string Stmtrestart { get; set; }
        [XmlAttribute(AttributeName = "stmtformat")]
        public string Stmtformat { get; set; }
        [XmlAttribute(AttributeName = "billcycle")]
        public string Billcycle { get; set; }
        [XmlAttribute(AttributeName = "fincharge")]
        public string Fincharge { get; set; }
    }

    [XmlRoot(ElementName = "accttypelist")]
    public class Accttypelist
    {
        [XmlElement(ElementName = "accttype")]
        public List<Accttype> Accttype { get; set; }
    }

    [XmlRoot(ElementName = "Results")]
    public class AcctTypeResults
    {
        [XmlElement(ElementName = "accttypelist")]
        public Accttypelist Accttypelist { get; set; }
    }

    [XmlRoot(ElementName = "PPMDResults")]
    public class PpmLookUpAcctTypeResponse : IPpmResponse
    {
        [XmlElement(ElementName = "Results")]
        public AcctTypeResults Results { get; set; }
        [XmlElement(ElementName = "Error")]
        public string Error { get; set; }
        [XmlAttribute(AttributeName = "s")]
        public string S { get; set; }
        [XmlAttribute(AttributeName = "lst")]
        public string Lst { get; set; }
    }
}
