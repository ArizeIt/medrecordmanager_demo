using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedRecordManager.Models.UserRecord
{
    public class UserVm
    {
        public string FirstName { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
    }
}
