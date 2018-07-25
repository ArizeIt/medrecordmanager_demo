using System;
using System.Collections.Generic;

namespace UgentCareDate.Models
{
    public partial class ProgramConfig
    {
        public int AmdofficeKey { get; set; }
        public string ApiuserName { get; set; }
        public string Apipassword { get; set; }
        public string Apiuri { get; set; }
        public string Environment { get; set; }
        public int Id { get; set; }
        public string RunAs { get; set; }
        public bool Force { get; set; }
        public bool AmdSync { get; set; }
        public string FilePath { get; set; }
        public string FromEmailAddress { get; set; }
        public string TechEmailAddress { get; set; }
        public string ToEmailAddress { get; set; }
        public string CcemailAddress { get; set; }
        public string Smtpserver { get; set; }
        public string Smtpport { get; set; }
        public string Smtpusername { get; set; }
        public string PvfilePrefix { get; set; }
        public int OffSetDays { get; set; }
        public string AmdAppName { get; set; }
        public bool Enabled { get; set; }
        public bool ProcessSource { get; set; }
        public bool? SyncOnly { get; set; }
        public string Pvfilename { get; set; }
        public bool AdditionalCharge { get; set; }
        public string Smtppassword { get; set; }
    }
}
