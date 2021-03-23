using MedRecordManager.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UrgentCareData.Models;

namespace MedRecordManager
{
    public class DbIdentifier
    {
        private readonly RequestDelegate _next;
           
        public DbIdentifier(RequestDelegate next)
        {
            _next = next;
        }  

        public async Task Invoke(HttpContext httpContext, ConnectionDataContext connectionContext)
        {
           
            // Get tenant id from token
            var userId = httpContext.User.Identity.Name;
            // Set tenant id to httpContext.Items
            
            if (!string.IsNullOrWhiteSpace(userId))
            {
               if(!httpContext.User.HasClaim(ClaimTypes.Role, "SuperAdmin"))
                {
                    using (connectionContext)
                    {
                        var userCompany = connectionContext.UserCompany.FirstOrDefault(x => x.UserId == userId);

                        if (userCompany != null)
                        {
                            httpContext.Items["company"] = connectionContext.CompanyProfile.FirstOrDefault(x => x.Id == userCompany.CompanyId);
                        }

                    }
                }                                               
            }

            await _next.Invoke(httpContext);
        }
    }
}
