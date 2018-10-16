using System.Collections.Generic;
using System.Xml.Serialization;

namespace PracticeVelocityDomain.DTOs
{
    [XmlRoot(ElementName = "Payer")]
    public class Payer
    {
        [XmlElement(ElementName = "Short_Name")]
        public string Short_name { get; set; }
        [XmlElement(ElementName = "Payer_Num")]
        public string Payer_num { get; set; }
        [XmlElement(ElementName = "Class")]
        public string Class { get; set; }
        [XmlElement(ElementName = "Type")]
        public string Type { get; set; }
        [XmlElement(ElementName = "Priority")]
        public string Priority { get; set; }
        [XmlElement(ElementName = "Ins_Name")]
        public string Ins_Name { get; set; }
        [XmlElement(ElementName = "Ins_Address1")]
        public string Ins_Address1 { get; set; }
        [XmlElement(ElementName = "Ins_Address2")]
        public string Ins_Address2 { get; set; }
        [XmlElement(ElementName = "Ins_City")]
        public string Ins_City { get; set; }
        [XmlElement(ElementName = "Ins_State")]
        public string Ins_State { get; set; }
        [XmlElement(ElementName = "Ins_Zip")]
        public string Ins_Zip { get; set; }
        [XmlElement(ElementName = "Ins_Relationship")]
        public string Ins_Relationship { get; set; }
        [XmlElement(ElementName = "Prim_Name")]
        public string Prim_Name { get; set; }
        [XmlElement(ElementName = "Prim_Address1")]
        public string Prim_Address1 { get; set; }
        [XmlElement(ElementName = "Prim_Address2")]
        public string Prim_Address2 { get; set; }
        [XmlElement(ElementName = "Prim_City")]
        public string Prim_City { get; set; }
        [XmlElement(ElementName = "Prim_State")]
        public string Prim_State { get; set; }
        [XmlElement(ElementName = "Prim_Zip")]
        public string Prim_Zip { get; set; }
        [XmlElement(ElementName = "Prim_Phone")]
        public string Prim_Phone { get; set; }
        [XmlElement(ElementName = "Group_ID")]
        public string Group_ID { get; set; }
        [XmlElement(ElementName = "Member_ID")]
        public string Member_ID { get; set; }
        [XmlElement(ElementName = "Relationship")]
        public string Relationship { get; set; }
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "Address1")]
        public string Address1 { get; set; }
        [XmlElement(ElementName = "Address2")]
        public string Address2 { get; set; }
        [XmlElement(ElementName = "City")]
        public string City { get; set; }
        [XmlElement(ElementName = "State")]
        public string State { get; set; }
        [XmlElement(ElementName = "Zip")]
        public string Zip { get; set; }
        [XmlElement(ElementName = "Phone")]
        public string Phone { get; set; }
    }

    [XmlRoot(ElementName = "payerInformation")]
    public class PvPayerInformation
    {
        [XmlElement(ElementName = "Payer")]
        public List<Payer> Payers { get; set; }
    }

    [XmlRoot(ElementName = "GuarantorInformation")]
    public class GuarantorInformation
    {
        [XmlElement(ElementName = "Payer")]
        public Payer Payer { get; set; }
    }

    [XmlRoot(ElementName = "PatientDocument")]
    public class PvPatientDocument
    {
        [XmlElement(ElementName = "FileName")]
        public string FileName { get; set; }
        [XmlElement(ElementName = "FileType")]
        public string FileType { get; set; }
        [XmlElement(ElementName = "NumberOfPages")]
        public string NumberOfPages { get; set; }
        [XmlElement(ElementName = "LastVerifiedBy")]
        public string LastVerifiedBy { get; set; }
        [XmlElement(ElementName = "LastVerifiedOn")]
        public string LastVerifiedOn { get; set; }
        [XmlElement(ElementName = "PatientDocument")]
        public List<PvPatientDocument> PatientDocuments { get; set; }
    }

    [XmlRoot(ElementName = "PvChartDocument")]
    public class PvChartDocument
    {
        [XmlElement(ElementName = "FileName")]
        public string FileName { get; set; }
        [XmlElement(ElementName = "FileType")]
        public string FileType { get; set; }
        [XmlElement(ElementName = "NumberOfPages")]
        public string NumberOfPages { get; set; }
        [XmlElement(ElementName = "LastUpdatedOn")]
        public string LastUpdatedOn { get; set; }
        [XmlElement(ElementName = "LastUpdatedBy")]
        public string LastUpdatedBy { get; set; }
    }

    [XmlRoot(ElementName = "Chart")]
    public class Chart
    {
        [XmlElement(ElementName = "DischargedDate")]
        public string DischargedDate { get; set; }
        [XmlElement(ElementName = "DischargedBy")]
        public string DischargedBy { get; set; }
        [XmlElement(ElementName = "SignOffSealedDate")]
        public string SignOffSealedDate { get; set; }
        [XmlElement(ElementName = "SignedOffSealedBy")]
        public string SignedOffSealedBy { get; set; }
        [XmlElement(ElementName = "ChartDocument")]
        public PvChartDocument PvChartDocument { get; set; }
    }

    [XmlRoot(ElementName = "MedicalRecord")]
    public class MedicalRecord
    {
        [XmlElement(ElementName = "Chart")]
        public Chart Chart { get; set; }
    }

    [XmlRoot(ElementName = "Patient_Information")]
    public class Patient_Information
    {
        [XmlElement(ElementName = "PayerInformation")]
        public PvPayerInformation PvPayerInformation { get; set; }
        [XmlElement(ElementName = "GuarantorInformation")]
        public GuarantorInformation GuarantorInformation { get; set; }
        [XmlElement(ElementName = "PatientDocument")]
        public PvPatientDocument PvPatientDocument { get; set; }
        [XmlElement(ElementName = "MedicalRecord")]
        public MedicalRecord MedicalRecord { get; set; }
        [XmlAttribute(AttributeName = "Pat_Num")]
        public string Pat_NUM { get; set; }
        [XmlAttribute(AttributeName = "SSN")]
        public string SSN { get; set; }
        [XmlAttribute(AttributeName = "First_Name")]
        public string First_Name { get; set; }
        [XmlAttribute(AttributeName = "Last_Name")]
        public string Last_Name { get; set; }
        [XmlAttribute(AttributeName = "Middle_Name")]
        public string Middle_Name { get; set; }
        [XmlAttribute(AttributeName = "Address1")]
        public string Address1 { get; set; }
        [XmlAttribute(AttributeName = "Address2")]
        public string Address2 { get; set; }
        [XmlAttribute(AttributeName = "City")]
        public string City { get; set; }
        [XmlAttribute(AttributeName = "State")]
        public string State { get; set; }
        [XmlAttribute(AttributeName = "Zip")]
        public string Zip { get; set; }
        [XmlAttribute(AttributeName = "Birthday")]
        public string Birthday { get; set; }
        [XmlAttribute(AttributeName = "Sex")]
        public string Sex { get; set; }
        [XmlAttribute(AttributeName = "Email")]
        public string Email { get; set; }
        [XmlAttribute(AttributeName = "Cell_Phone")]
        public string Cell_Phone { get; set; }
        [XmlAttribute(AttributeName = "Home_Phone")]
        public string Home_Phone { get; set; }
        [XmlAttribute(AttributeName = "Proc_Codes")]
        public string Proc_codes { get; set; }
    }

    [XmlRoot(ElementName = "Log_Record")]
    public class Log_Record
    {
        [XmlElement(ElementName = "Patient_Information")]
        public Patient_Information Patient_Information { get; set; }
        [XmlAttribute(AttributeName = "Svc_Date")]
        public string Svc_date { get; set; }
        [XmlAttribute(AttributeName = "Clinic")]
        public string Clinic { get; set; }
        [XmlAttribute(AttributeName = "Log_Num")]
        public string Log_Num { get; set; }
        [XmlAttribute(AttributeName = "Last_Upd_UserID")]
        public string Last_Upd_UserID { get; set; }
        [XmlAttribute(AttributeName = "Last_Upd_DateTime")]
        public string Last_Upd_DateTime { get; set; }
        [XmlAttribute(AttributeName = "Diag_Codes")]
        public string Diag_Codes { get; set; }
        [XmlAttribute(AttributeName = "Diag_Codes_icd10")]
        public string Diag_Codes_icd10 { get; set; }
        [XmlAttribute(AttributeName = "VisitType")]
        public string VisitType { get; set; }
        [XmlAttribute(AttributeName = "EM_code")]
        public string EM_code { get; set; }
        [XmlAttribute(AttributeName = "Physican_ID")]
        public string Physican_ID { get; set; }
        [XmlAttribute(AttributeName = "Physican")]
        public string Physican { get; set; }
        [XmlAttribute(AttributeName = "Copay_Amt")]
        public string Copay_Amt { get; set; }
        [XmlAttribute(AttributeName = "Copay_Type")]
        public string Copay_Type { get; set; }
        [XmlAttribute(AttributeName = "Copay_Notes")]
        public string Copay_Notes { get; set; }
        [XmlAttribute(AttributeName = "Prev_Pay_Amt")]
        public string Prev_Pay_Amt { get; set; }
        [XmlAttribute(AttributeName = "Prev_Pay_Type")]
        public string Prev_Pay_Type { get; set; }
        [XmlAttribute(AttributeName = "Prev_Pay_Notes")]
        public string Prev_Pay_Notes { get; set; }
        [XmlAttribute(AttributeName = "Curr_Pay_Amt")]
        public string Curr_Pay_Amt { get; set; }
        [XmlAttribute(AttributeName = "Curr_Pay_Type")]
        public string Curr_Pay_Type { get; set; }
        [XmlAttribute(AttributeName = "Curr_Pay_Notes")]
        public string Curr_Pay_Notes { get; set; }
        [XmlAttribute(AttributeName = "Time_In")]
        public string Time_In { get; set; }
        [XmlAttribute(AttributeName = "Time_out")]
        public string Time_out { get; set; }
        [XmlAttribute(AttributeName = "Notes")]
        public string Notes { get; set; }
    }

    [XmlRoot(ElementName = "root")]
    public class NewRecord
    {
        [XmlElement(ElementName = "Log_Record")]
        public List<Log_Record> Log_Record { get; set; }
    }
}
