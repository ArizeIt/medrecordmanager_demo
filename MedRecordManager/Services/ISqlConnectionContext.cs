using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using UrgentCareData.Models;

namespace MedRecordManager
{
    public interface ISqlConnectionContext
    {
        string GetConnectionString();
    }

    public class SqlConnectionContext : ISqlConnectionContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _config;
        public SqlConnectionContext(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _config = configuration;
        }
        public string GetConnectionString()
        {
            var company = (CompanyProfile)_httpContextAccessor.HttpContext.Items["company"];
            if(company != null)
            {
                return company.DbConnection;
            }
            return _config.GetConnectionString("DefaultConnection");
        }
    }
}