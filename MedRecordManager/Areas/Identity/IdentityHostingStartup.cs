using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(MedRecordManager.Areas.Identity.IdentityHostingStartup))]
namespace MedRecordManager.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}