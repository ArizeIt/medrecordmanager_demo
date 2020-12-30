using System.ComponentModel.DataAnnotations;

namespace MedRecordManager.Models.UserRecord
{
    public class CreateRole
    {

        [Required]
        public string RoleName { get; set; }
    }

    public enum Roles
    {
        SuperAdmin,
        CompanyAdmin,
        OfficeAdmin,
        ClinicAdmin,
        CompanyPowerUser,
        OfficePowerUser,
        ClinicPowerUser
    }
}
