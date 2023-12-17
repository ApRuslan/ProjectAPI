using planningsoverzicht.Entities;
using System.Text.Json;
using System.Xml;
using Azure.Identity;
using Microsoft.Extensions.Configuration;

namespace planningsoverzicht
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((context, config) =>
            {}).ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}