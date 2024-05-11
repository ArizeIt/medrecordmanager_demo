using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using UrgentCareData.Models;

namespace MedRecordManager
{
    public interface ISqlConnectionContext
    {
        string GetConnectionString();
        string GetDefaultConnectionString();
        string BuildConnectionString(string connectionString);
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
            if (company != null)
            {
                return company.DbConnection;
            }
            return GetDefaultConnectionString();
        }

        public string GetDefaultConnectionString()
        {
            return BuildConnectionString(_config.GetConnectionString("DefaultConnection"));
        }

        public string BuildConnectionString(string connectionString)
        {
            return connectionString.Replace("{userId}", "remoteUser").Replace("{password}", "Sm@llfish12");
        }
    }
}