using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseAkka("test", ReadConfigFile("config.hocon")
                , (sp, sys) =>
                {
                });

        private static string ReadConfigFile(string hoconFileName)
        {
            var assemblyFilePath = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase).LocalPath;
            var assemblyDirectoryPath = Path.GetDirectoryName(assemblyFilePath);
            var hoconFilePath = $@"{assemblyDirectoryPath}{Path.DirectorySeparatorChar}{hoconFileName}";

            return File.ReadAllText(hoconFilePath);
        }
    }
}
