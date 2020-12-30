using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Responses
{
    [XmlRoot(ElementName = "batch")]
    public class Batch
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "number")]
        public string Number { get; set; }
        [XmlAttribute(AttributeName = "status")]
        public string Status { get; set; }
        [XmlAttribute(AttributeName = "postingdate")]
        public string Postingdate { get; set; }
        [XmlAttribute(AttributeName = "servicedate")]
        public string Servicedate { get; set; }
        [XmlAttribute(AttributeName = "closeddate")]
        public string Closeddate { get; set; }
        [XmlAttribute(AttributeName = "owner")]
        public string Owner { get; set; }
        [XmlAttribute(AttributeName = "charges")]
        public string Charges { get; set; }
        [XmlAttribute(AttributeName = "payments")]
        public string Payments { get; set; }
        [XmlAttribute(AttributeName = "writeoffs")]
        public string Writeoffs { get; set; }
        [XmlAttribute(AttributeName = "receivedate")]
        public string Receivedate { get; set; }
    }

    [XmlRoot(ElementName = "batchlist")]
    public class Batchlist
    {
        [XmlElement(ElementName = "batch")]
        public Batch Batch { get; set; }
    }

    [XmlRoot(ElementName = "Results")]
    public class BatchResults
    {
        [XmlElement(ElementName = "batchlist")]
        public Batchlist Batchlist { get; set; }
    }

    [XmlRoot(ElementName = "PPMDResults")]
    public class PpmNewBatchResponse : IPpmResponse
    {
        [XmlElement(ElementName = "Results")]
        public BatchResults Results { get; set; }
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
