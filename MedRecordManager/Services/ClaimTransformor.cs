using MedRecordManager.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MedRecordManager.Services
{
    public class ClaimsTransformer : IClaimsTransformation
    {
        private readonly ApplicationDbContext _context;
        private readonly AppAdminContext _adminContext;
        public ClaimsTransformer(ApplicationDbContext context, AppAdminContext adminContext)
        {
            _context = context;
            _adminContext = adminContext;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var existingClaimsIdentity = (ClaimsIdentity)principal.Identity;
            var currentUserName = existingClaimsIdentity.Name;

            // Initialize a new list of claims for the new identity
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, currentUserName),
            // Potentially add more from the existing claims here
        };

            // Find the user in the DB
            // Add as many role claims as they have roles in the DB
            IdentityUser user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == currentUserName);
            if (user != null)
            {
               
                var CompanyNames = _adminContext.UserCompany.Where(x=>x.UserName == currentUserName);

                claims.AddRange(CompanyNames.Select(x => new Claim(ClaimTypes.PrimaryGroupSid, x.CompanyId.ToString())));
            }




            // Build and return the new principal
            var newClaimsIdentity = new ClaimsIdentity(claims, existingClaimsIdentity.AuthenticationType);
            return new ClaimsPrincipal(newClaimsIdentity);
        }
    }
}
