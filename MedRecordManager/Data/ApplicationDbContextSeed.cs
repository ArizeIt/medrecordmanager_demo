using MedRecordManager.Models.UserRecord;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedRecordManager.Data
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.ClinicAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.CompanyAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.OfficeAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.CompanyPowerUser.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.ClinicPowerUser.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.OfficePowerUser.ToString()));
        }

        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
              
                Email = "Danny@gmail.com",
                FirstName = "Danny",
                LastName = "Li",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                   
                    await userManager.AddToRoleAsync(defaultUser,Roles.ClinicAdmin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.OfficeAdmin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.CompanyAdmin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
                }

            }
        }
    }
}
