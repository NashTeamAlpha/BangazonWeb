using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace BangazonWeb
{
    //Class Name: Program
    //Author: Grant Regnier
    //Purpose of the class: The purpose of this class is to Build our application and host in on our server we are building. Using AspNetCore Hosting and kestrel we are assembling our host from our config and other Extensions then running it on line 28.
    //Methods in Class: Main
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddEnvironmentVariables(prefix: "ASPNETCORE_")
                .Build();

            var host = new WebHostBuilder()
                .UseConfiguration(config)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
