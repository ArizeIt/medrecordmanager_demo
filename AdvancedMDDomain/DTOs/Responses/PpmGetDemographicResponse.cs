using System.Collections.Generic;
using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Responses
{

    [XmlRoot(ElementName = "address")]
    public class DemoAddress
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
    public class DemoContactinfo
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
        [XmlAttribute(AttributeName = "emailverificationstatus")]
        public string Emailverificationstatus { get; set; }
        [XmlAttribute(AttributeName = "preferredcommunicationfid")]
        public string Preferredcommunicationfid { get; set; }
        [XmlAttribute(AttributeName = "confidentialcommunicationfid")]
        public string Confidentialcommunicationfid { get; set; }
        [XmlAttribute(AttributeName = "communicationnote")]
        public string Communicationnote { get; set; }
        [XmlAttribute(AttributeName = "fax")]
        public string Fax { get; set; }
    }

    [XmlRoot(ElementName = "arbucket")]
    public class DemoArbucket
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
    public class DemoArbucketlist
    {
        [XmlElement(ElementName = "arbucket")]
        public List<DemoArbucket> Arbucket { get; set; }
    }

    [XmlRoot(ElementName = "insnote")]
    public class Insnote
    {
        [XmlAttribute(AttributeName = "insnotefid")]
        public string Insnotefid { get; set; }
    }

    [XmlRoot(ElementName = "insplan")]
    public class DemoInsplan
    {
        [XmlElement(ElementName = "insnote")]
        public Insnote Insnote { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "carrier")]
        public string Carrier { get; set; }
        [XmlAttribute(AttributeName = "begindate")]
        public string Begindate { get; set; }
        [XmlAttribute(AttributeName = "enddate")]
        public string Enddate { get; set; }
        [XmlAttribute(AttributeName = "copay")]
        public string Copay { get; set; }
        [XmlAttribute(AttributeName = "copaytype")]
        public string Copaytype { get; set; }
        [XmlAttribute(AttributeName = "coverage")]
        public string Coverage { get; set; }
        [XmlAttribute(AttributeName = "subscriber")]
        public string Subscriber { get; set; }
        [XmlAttribute(AttributeName = "subscribernum")]
        public string Subscribernum { get; set; }
        [XmlAttribute(AttributeName = "relationship")]
        public string Relationship { get; set; }
        [XmlAttribute(AttributeName = "hipaarelationship")]
        public string Hipaarelationship { get; set; }
        [XmlAttribute(AttributeName = "sequence")]
        public string Sequence { get; set; }
        [XmlAttribute(AttributeName = "grpname")]
        public string Grpname { get; set; }
        [XmlAttribute(AttributeName = "grpnum")]
        public string Grpnum { get; set; }
        [XmlAttribute(AttributeName = "mspcode")]
        public string Mspcode { get; set; }
        [XmlAttribute(AttributeName = "payerid")]
        public string Payerid { get; set; }
        [XmlAttribute(AttributeName = "deductible")]
        public string Deductible { get; set; }
        [XmlAttribute(AttributeName = "deductiblemet")]
        public string Deductiblemet { get; set; }
        [XmlAttribute(AttributeName = "yearendmonth")]
        public string Yearendmonth { get; set; }
        [XmlAttribute(AttributeName = "lifetime")]
        public string Lifetime { get; set; }
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
        [XmlAttribute(AttributeName = "claimcount")]
        public string Claimcount { get; set; }
        [XmlAttribute(AttributeName = "eligibilityvalidation")]
        public string Eligibilityvalidation { get; set; }
        [XmlAttribute(AttributeName = "eligibilityvalidationfollowup")]
        public string Eligibilityvalidationfollowup { get; set; }
    }

    [XmlRoot(ElementName = "insplanlist")]
    public class Insplanlist
    {
        [XmlElement(ElementName = "insplan")]
        public DemoInsplan Insplan { get; set; }
    }

    [XmlRoot(ElementName = "refplan")]
    public class DemoRefplan
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "refprov")]
        public string Refprov { get; set; }
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlAttribute(AttributeName = "refplanstatus")]
        public string Refplanstatus { get; set; }
        [XmlAttribute(AttributeName = "reason")]
        public string Reason { get; set; }
        [XmlAttribute(AttributeName = "preauthreq")]
        public string Preauthreq { get; set; }
        [XmlAttribute(AttributeName = "preauth")]
        public string Preauth { get; set; }
        [XmlAttribute(AttributeName = "refnum")]
        public string Refnum { get; set; }
        [XmlAttribute(AttributeName = "facility")]
        public string Facility { get; set; }
        [XmlAttribute(AttributeName = "proccode")]
        public string Proccode { get; set; }
        [XmlAttribute(AttributeName = "diagcodes")]
        public string Diagcodes { get; set; }
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
        [XmlAttribute(AttributeName = "status")]
        public string Status { get; set; }
        [XmlAttribute(AttributeName = "sequence")]
        public string Sequence { get; set; }
        [XmlAttribute(AttributeName = "notes")]
        public string Notes { get; set; }
        [XmlAttribute(AttributeName = "ismain")]
        public string Ismain { get; set; }
    }

    [XmlRoot(ElementName = "refplanlist")]
    public class DemoRefplanlist
    {
        [XmlElement(ElementName = "refplan")]
        public DemoRefplan Refplan { get; set; }
    }

    [XmlRoot(ElementName = "memo")]
    public class Memo
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlAttribute(AttributeName = "enddate")]
        public string Enddate { get; set; }
        [XmlAttribute(AttributeName = "changedat")]
        public string Changedat { get; set; }
        [XmlAttribute(AttributeName = "created")]
        public string Created { get; set; }
        [XmlAttribute(AttributeName = "createuser")]
        public string Createuser { get; set; }
        [XmlAttribute(AttributeName = "revoked")]
        public string Revoked { get; set; }
        [XmlAttribute(AttributeName = "revokedby")]
        public string Revokedby { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "memolist")]
    public class Memolist
    {
        [XmlElement(ElementName = "memo")]
        public Memo Memo { get; set; }
    }

    [XmlRoot(ElementName = "category")]
    public class DemoCategory
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "groupcode")]
        public string Groupcode { get; set; }
        [XmlAttribute(AttributeName = "groupname")]
        public string Groupname { get; set; }
        [XmlAttribute(AttributeName = "filetype")]
        public string Filetype { get; set; }
    }

    [XmlRoot(ElementName = "categorylist")]
    public class DemoCategorylist
    {
        [XmlElement(ElementName = "category")]
        public DemoCategory Category { get; set; }
    }

    [XmlRoot(ElementName = "file")]
    public class DemoFile
    {
        [XmlElement(ElementName = "categorylist")]
        public DemoCategorylist Categorylist { get; set; }
        [XmlElement(ElementName = "revisionlist")]
        public string Revisionlist { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "description")]
        public string Description { get; set; }
        [XmlAttribute(AttributeName = "filename")]
        public string Filename { get; set; }
        [XmlAttribute(AttributeName = "filelocation")]
        public string Filelocation { get; set; }
        [XmlAttribute(AttributeName = "filetype")]
        public string Filetype { get; set; }
        [XmlAttribute(AttributeName = "fileext")]
        public string Fileext { get; set; }
        [XmlAttribute(AttributeName = "filesize")]
        public string Filesize { get; set; }
        [XmlAttribute(AttributeName = "patientid")]
        public string Patientid { get; set; }
        [XmlAttribute(AttributeName = "patientname")]
        public string Patientname { get; set; }
        [XmlAttribute(AttributeName = "patientchartnumber")]
        public string Patientchartnumber { get; set; }
        [XmlAttribute(AttributeName = "profileid")]
        public string Profileid { get; set; }
        [XmlAttribute(AttributeName = "profilecode")]
        public string Profilecode { get; set; }
        [XmlAttribute(AttributeName = "profilename")]
        public string Profilename { get; set; }
        [XmlAttribute(AttributeName = "visitid")]
        public string Visitid { get; set; }
        [XmlAttribute(AttributeName = "facilityid")]
        public string Facilityid { get; set; }
        [XmlAttribute(AttributeName = "referringproviderid")]
        public string Referringproviderid { get; set; }
        [XmlAttribute(AttributeName = "referringprovidername")]
        public string Referringprovidername { get; set; }
        [XmlAttribute(AttributeName = "referringprovidercode")]
        public string Referringprovidercode { get; set; }
        [XmlAttribute(AttributeName = "dos")]
        public string Dos { get; set; }
        [XmlAttribute(AttributeName = "createdby")]
        public string Createdby { get; set; }
        [XmlAttribute(AttributeName = "createdat")]
        public string Createdat { get; set; }
        [XmlAttribute(AttributeName = "printedby")]
        public string Printedby { get; set; }
        [XmlAttribute(AttributeName = "printedat")]
        public string Printedat { get; set; }
        [XmlAttribute(AttributeName = "reviewedby")]
        public string Reviewedby { get; set; }
        [XmlAttribute(AttributeName = "reviewedat")]
        public string Reviewedat { get; set; }
        [XmlAttribute(AttributeName = "approvedby")]
        public string Approvedby { get; set; }
        [XmlAttribute(AttributeName = "approvedat")]
        public string Approvedat { get; set; }
        [XmlAttribute(AttributeName = "lockedby")]
        public string Lockedby { get; set; }
        [XmlAttribute(AttributeName = "lockedat")]
        public string Lockedat { get; set; }
        [XmlAttribute(AttributeName = "changedby")]
        public string Changedby { get; set; }
        [XmlAttribute(AttributeName = "changedat")]
        public string Changedat { get; set; }
        [XmlAttribute(AttributeName = "revision")]
        public string Revision { get; set; }
        [XmlAttribute(AttributeName = "zipmode")]
        public string Zipmode { get; set; }
        [XmlAttribute(AttributeName = "serverfile")]
        public string Serverfile { get; set; }
        [XmlAttribute(AttributeName = "charttype")]
        public string Charttype { get; set; }
    }

    [XmlRoot(ElementName = "filelist")]
    public class DemoFilelist
    {
        [XmlElement(ElementName = "file")]
        public List<DemoFile> File { get; set; }
    }

    [XmlRoot(ElementName = "patient")]
    public class DemoPatient
    {
        [XmlElement(ElementName = "address")]
        public Requests.Address Address { get; set; }
        [XmlElement(ElementName = "contactinfo")]
        public Requests.Contactinfo Contactinfo { get; set; }
        [XmlElement(ElementName = "arbucketlist")]
        public DemoArbucketlist Arbucketlist { get; set; }
        [XmlElement(ElementName = "insplanlist")]
        public Insplanlist Insplanlist { get; set; }
        [XmlElement(ElementName = "refplanlist")]
        public DemoRefplanlist Refplanlist { get; set; }
        [XmlElement(ElementName = "memolist")]
        public Memolist Memolist { get; set; }
        [XmlElement(ElementName = "filelist")]
        public DemoFilelist Filelist { get; set; }
        [XmlElement(ElementName = "visitlist")]
        public string Visitlist { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "importid")]
        public string Importid { get; set; }
        [XmlAttribute(AttributeName = "dob")]
        public string Dob { get; set; }
        [XmlAttribute(AttributeName = "deceased")]
        public string Deceased { get; set; }
        [XmlAttribute(AttributeName = "inactive")]
        public string Inactive { get; set; }
        [XmlAttribute(AttributeName = "inactivestatus")]
        public string Inactivestatus { get; set; }
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
        [XmlAttribute(AttributeName = "title")]
        public string Title { get; set; }
        [XmlAttribute(AttributeName = "relationship")]
        public string Relationship { get; set; }
        [XmlAttribute(AttributeName = "hipaarelationship")]
        public string Hipaarelationship { get; set; }
        [XmlAttribute(AttributeName = "languageid")]
        public string Languageid { get; set; }
        [XmlAttribute(AttributeName = "ethnicityid")]
        public string Ethnicityid { get; set; }
        [XmlAttribute(AttributeName = "language")]
        public string Language { get; set; }
        [XmlAttribute(AttributeName = "ethnicity")]
        public string Ethnicity { get; set; }
        [XmlAttribute(AttributeName = "races")]
        public string Races { get; set; }
        [XmlAttribute(AttributeName = "recalcbuckets")]
        public string Recalcbuckets { get; set; }
        [XmlAttribute(AttributeName = "additionalmrn")]
        public string Additionalmrn { get; set; }
    }

    [XmlRoot(ElementName = "patientlist")]
    public class DemoPatientlist
    {
        [XmlElement(ElementName = "patient")]
        public DemoPatient Patient { get; set; }
    }

    [XmlRoot(ElementName = "familymember")]
    public class DemoFamilymember
    {
        [XmlElement(ElementName = "contactinfo")]
        public Requests.Contactinfo Contactinfo { get; set; }
        [XmlElement(ElementName = "address")]
        public Requests.Address Address { get; set; }
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
    public class DemoFamilymemberlist
    {
        [XmlElement(ElementName = "familymember")]
        public DemoFamilymember Familymember { get; set; }
    }

    [XmlRoot(ElementName = "respparty")]
    public class DemoRespparty
    {
        [XmlElement(ElementName = "address")]
        public Requests.Address Address { get; set; }
        [XmlElement(ElementName = "contactinfo")]
        public Requests.Contactinfo Contactinfo { get; set; }
        [XmlElement(ElementName = "creditcardonfile")]
        public string Creditcardonfile { get; set; }
        [XmlElement(ElementName = "arbucketlist")]
        public DemoArbucketlist Arbucketlist { get; set; }
        [XmlElement(ElementName = "familymemberlist")]
        public DemoFamilymemberlist Familymemberlist { get; set; }
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
        [XmlAttribute(AttributeName = "lastbillingcaption")]
        public string Lastbillingcaption { get; set; }
        [XmlAttribute(AttributeName = "lastbillingtip")]
        public string Lastbillingtip { get; set; }
        [XmlAttribute(AttributeName = "paymentplanbalance")]
        public string Paymentplanbalance { get; set; }
    }

    [XmlRoot(ElementName = "resppartylist")]
    public class DemoResppartylist
    {
        [XmlElement(ElementName = "respparty")]
        public DemoRespparty Respparty { get; set; }
    }

    [XmlRoot(ElementName = "accttype")]
    public class DemoAccttype
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }
    }

    [XmlRoot(ElementName = "accttypelist")]
    public class DemoAccttypelist
    {
        [XmlElement(ElementName = "accttype")]
        public DemoAccttype Accttype { get; set; }
    }

    [XmlRoot(ElementName = "finclass")]
    public class DemoFinclass
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }
        [XmlAttribute(AttributeName = "billins")]
        public string Billins { get; set; }
        [XmlAttribute(AttributeName = "billlab")]
        public string Billlab { get; set; }
        [XmlAttribute(AttributeName = "ca")]
        public string Ca { get; set; }
        [XmlAttribute(AttributeName = "responsible")]
        public string Responsible { get; set; }
        [XmlAttribute(AttributeName = "acceptassign")]
        public string Acceptassign { get; set; }
        [XmlAttribute(AttributeName = "allowablewocode")]
        public string Allowablewocode { get; set; }
        [XmlAttribute(AttributeName = "autowo")]
        public string Autowo { get; set; }
        [XmlAttribute(AttributeName = "autowocode")]
        public string Autowocode { get; set; }
        [XmlAttribute(AttributeName = "riskwocode")]
        public string Riskwocode { get; set; }
        [XmlAttribute(AttributeName = "riskpercent")]
        public string Riskpercent { get; set; }
        [XmlAttribute(AttributeName = "accttype")]
        public string Accttype { get; set; }
        [XmlAttribute(AttributeName = "allowfeesched")]
        public string Allowfeesched { get; set; }
    }

    [XmlRoot(ElementName = "finclasslist")]
    public class DemoFinclasslist
    {
        [XmlElement(ElementName = "finclass")]
        public List<DemoFinclass> Finclass { get; set; }
    }

    [XmlRoot(ElementName = "profile")]
    public class DemoProfile
    {
        [XmlElement(ElementName = "finclasslist")]
        public DemoFinclasslist Finclasslist { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "fullname")]
        public string Fullname { get; set; }
        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }
        [XmlAttribute(AttributeName = "chargefeesched")]
        public string Chargefeesched { get; set; }
        [XmlAttribute(AttributeName = "fullinactive")]
        public string Fullinactive { get; set; }
        [XmlAttribute(AttributeName = "status")]
        public string Status { get; set; }
    }

    [XmlRoot(ElementName = "profilelist")]
    public class DemoProfilelist
    {
        [XmlElement(ElementName = "profile")]
        public DemoProfile Profile { get; set; }
    }

    [XmlRoot(ElementName = "carrier")]
    public class DemoCarrier
    {
        [XmlElement(ElementName = "address")]
        public Requests.Address Address { get; set; }
        [XmlElement(ElementName = "contactinfo")]
        public Requests.Contactinfo Contactinfo { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "preauthphone")]
        public string Preauthphone { get; set; }
        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }
        [XmlAttribute(AttributeName = "eligibilityphone")]
        public string Eligibilityphone { get; set; }
        [XmlAttribute(AttributeName = "billzerodollarclaims")]
        public string Billzerodollarclaims { get; set; }
        [XmlAttribute(AttributeName = "dme")]
        public string Dme { get; set; }
        [XmlAttribute(AttributeName = "icd10startdate")]
        public string Icd10startdate { get; set; }
    }

    [XmlRoot(ElementName = "carrierlist")]
    public class DemoCarrierlist
    {
        [XmlElement(ElementName = "carrier")]
        public List<DemoCarrier> Carriers { get; set; }
    }

    [XmlRoot(ElementName = "refprovider")]
    public class DemoRefprovider
    {
        [XmlElement(ElementName = "address")]
        public Requests.Address Address { get; set; }
        [XmlElement(ElementName = "contactinfo")]
        public Requests.Contactinfo Contactinfo { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }
        [XmlAttribute(AttributeName = "specialty")]
        public string Specialty { get; set; }
        [XmlAttribute(AttributeName = "practicename")]
        public string Practicename { get; set; }
    }

    [XmlRoot(ElementName = "refproviderlist")]
    public class DemoRefproviderlist
    {
        [XmlElement(ElementName = "refprovider")]
        public DemoRefprovider Refprovider { get; set; }
    }

    [XmlRoot(ElementName = "Results")]
    public class DemoResults
    {
        [XmlElement(ElementName = "patientlist")]
        public DemoPatientlist Patientlist { get; set; }
        [XmlElement(ElementName = "resppartylist")]
        public Requests.Resppartylist Resppartylist { get; set; }
        [XmlElement(ElementName = "accttypelist")]
        public DemoAccttypelist Accttypelist { get; set; }
        [XmlElement(ElementName = "profilelist")]
        public DemoProfilelist Profilelist { get; set; }
        [XmlElement(ElementName = "carrierlist")]
        public DemoCarrierlist Carrierlist { get; set; }
        [XmlElement(ElementName = "proccodelist")]
        public string Proccodelist { get; set; }
        [XmlElement(ElementName = "modcodelist")]
        public string Modcodelist { get; set; }
        [XmlElement(ElementName = "diagcodelist")]
        public string Diagcodelist { get; set; }
        [XmlElement(ElementName = "facilitylist")]
        public string Facilitylist { get; set; }
        [XmlElement(ElementName = "statuslist")]
        public string Statuslist { get; set; }
        [XmlElement(ElementName = "refproviderlist")]
        public DemoRefproviderlist Refproviderlist { get; set; }
        [XmlElement(ElementName = "notelist")]
        public string Notelist { get; set; }
    }

    [XmlRoot(ElementName = "PPMDResults")]
    public class PpmGetDemographicResponse : IPpmResponse
    {
        [XmlElement(ElementName = "Results")]
        public DemoResults Results { get; set; }
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
