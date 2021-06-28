using Microsoft.AspNetCore.Identity;

namespace MedRecordManager.Models.UserRecord
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Company { get; set; }

        public int UsernameChangeLimit { get; set; } = 10;
        public byte[] ProfilePicture { get; set; }
    }
}
