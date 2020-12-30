using System;

namespace UrgentCareData.Models
{
    public partial class PatientDocument
    {
        public string FileName { get; set; }
        public int FileType { get; set; }
        public int NumofPages { get; set; }
        public string LastVerifedBy { get; set; }
        public DateTime LastVerifiedOn { get; set; }
        public byte[] FileImage { get; set; }
        public int VisitId { get; set; }
        public int PatDocId { get; set; }
        public string AmdFileId { get; set; }

        public virtual Visit Visit { get; set; }
    }
}
