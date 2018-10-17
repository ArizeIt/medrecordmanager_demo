using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AdvancedMDDomain.DTOs.Responses
{
    [XmlRoot(ElementName = "category")]
    public class Category
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
    public class Categorylist
    {
        [XmlElement(ElementName = "category")]
        public Category Category { get; set; }
    }

    [XmlRoot(ElementName = "file")]
    public class ResponseFile
    {
        [XmlElement(ElementName = "categorylist")]
        public Categorylist Categorylist { get; set; }
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
    public class Filelist
    {
        [XmlElement(ElementName = "file")]
        public ResponseFile File { get; set; }
    }

    [XmlRoot(ElementName = "Results")]
    public class UploadFileResults
    {
        [XmlElement(ElementName = "filelist")]
        public Filelist Filelist { get; set; }
        [XmlElement(ElementName = "updatedlist")]
        public string Updatedlist { get; set; }
    }

    [XmlRoot(ElementName = "PPMDResults")]
    public class PpmUploadFileResponse :IPpmResponse
    {
        [XmlElement(ElementName = "Results")]
        public UploadFileResults Results { get; set; }
        [XmlAttribute(AttributeName = "s")]
        public string S { get; set; }
        [XmlAttribute(AttributeName = "lst")]
        public string Lst { get; set; }

        [XmlIgnore]
        public string Error { get; set; }
    }

  
}
