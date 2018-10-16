using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Requests
{
    [XmlRoot(ElementName = "payment")]
    public class Payment
    {
        [XmlAttribute(AttributeName = "source")]
        public string Source { get; set; }
        [XmlAttribute(AttributeName = "profile")]
        public string Profile { get; set; }
        [XmlAttribute(AttributeName = "amount")]
        public string Amount { get; set; }
        [XmlAttribute(AttributeName = "carrier")]
        public string Carrier { get; set; }
    }

    [XmlRoot(ElementName = "unappliedpaymentlist")]
    public class Unappliedpaymentlist
    {
        [XmlElement(ElementName = "payment")]
        public Payment Payment { get; set; }
    }

    [XmlRoot(ElementName = "patient")]
    public class AddPaymentPatient
    {
        [XmlElement(ElementName = "unappliedpaymentlist")]
        public Unappliedpaymentlist Unappliedpaymentlist { get; set; }
        [XmlElement(ElementName = "unappliedwriteofflist")]
        public string Unappliedwriteofflist { get; set; }
        [XmlElement(ElementName = "chargelist")]
        public string Chargelist { get; set; }
        [XmlAttribute(AttributeName = "patientid")]
        public string Patientid { get; set; }
        [XmlAttribute(AttributeName = "paysource")]
        public string Paysource { get; set; }
        [XmlAttribute(AttributeName = "amount")]
        public string Amount { get; set; }
        [XmlAttribute(AttributeName = "paycode")]
        public string Paycode { get; set; }
        [XmlAttribute(AttributeName = "woamount")]
        public string Woamount { get; set; }
        [XmlAttribute(AttributeName = "paymethod")]
        public string Paymethod { get; set; }
        [XmlAttribute(AttributeName = "carrierid")]
        public string Carrierid { get; set; }
        [XmlAttribute(AttributeName = "depositdate")]
        public string Depositdate { get; set; }
        [XmlAttribute(AttributeName = "case_note")]
        public string CaseNote { get; set; }
        [XmlAttribute(AttributeName = "inclstmt")]
        public string Inclstmt { get; set; }
        [XmlAttribute(AttributeName = "postedby")]
        public string Postedby { get; set; }
        [XmlAttribute(AttributeName = "batch")]
        public string Batch { get; set; }
    }

    [XmlRoot(ElementName = "ppmdmsg")]
    public class PpmAddPaymentRequest:IPpmRequest
    {
        [XmlElement(ElementName = "patient")]
        public AddPaymentPatient Patient { get; set; }
        [XmlAttribute(AttributeName = "action")]
        public string Action { get; set; }
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
        [XmlAttribute(AttributeName = "msgtime")]
        public string Msgtime { get; set; }
        [XmlAttribute(AttributeName = "ltq")]
        public string Ltq { get; set; }
        [XmlAttribute(AttributeName = "checkout")]
        public string Checkout { get; set; }
        [XmlAttribute(AttributeName = "date")]
        public string Date { get; set; }
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

