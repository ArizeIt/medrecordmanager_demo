using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
