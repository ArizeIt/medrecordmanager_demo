using Microsoft.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace SourceLoader
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = new HostBuilder()

                .ConfigureServices((hostContext, services) =>
                {
                    services.AddLogging();
                   
                })
                .UseConsoleLifetime()
                .Build();
            await host.RunAsync();
        }
    }
}
