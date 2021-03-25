using MedRecordManager.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UrgentCareData.Models;

namespace MedRecordManager
{
    public class DbIdentifier
    {
        private readonly RequestDelegate _next;
        private readonly ISqlConnectionContext _connectionContext;
        public DbIdentifier(RequestDelegate next, ISqlConnectionContext connectionContext )
        {
            _next = next;
            _connectionContext = connectionContext;
        }  

        public async Task Invoke(HttpContext httpContext)
        {
            var _connectionData = new AppAdminContext(_connectionContext.GetDefaultConnectionString());
            // Get tenant id from token
            var userName = httpContext.User.Identity.Name;
            // Set tenant id to httpContext.Items
            
            if (!string.IsNullOrWhiteSpace(userName))
            {
               if(!httpContext.User.HasClaim(ClaimTypes.Role, "SuperAdmin"))
                {
                    using (_connectionData)
                    {
                        var userCompany = _connectionData.UserCompany.FirstOrDefault(x => x.UserName == userName);

                        if (userCompany != null)
                        {
                            var company = _connectionData.CompanyProfile.FirstOrDefault(x => x.Id == userCompany.CompanyId);
                            company.DbConnection = _connectionContext.BuildConnectionString(company.DbConnection);
                            httpContext.Items["company"] = _connectionData.CompanyProfile.FirstOrDefault(x => x.Id == userCompany.CompanyId);
                        }

                    }
                }                                               
            }

            await _next.Invoke(httpContext);
        }
    }
}
