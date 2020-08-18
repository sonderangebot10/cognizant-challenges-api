using System;

using Autofac.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using Serilog;

namespace Cognizant.ChallangesApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (environment == null)
            {
                throw new ApplicationException("ASPNETCORE_ENVIRONMENT not found");
            }
            Console.WriteLine($"Environment {environment}");
            CreateHostBuilder(args).Build()
                .Run();

            Log.Information("Cognizant API started");
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseIISIntegration();
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureAppConfiguration((builderContext, config) =>
                    {
                        var env = builderContext.HostingEnvironment;
                        config.AddJsonFile("appsettings.json", optional: false);
                        config.AddJsonFile(
                            $"appsettings.{env.EnvironmentName}.json",
                            optional: true);

                        config.AddJsonFile("autofac.json", optional: false);
                        config.AddJsonFile(
                            $"autofac.{env.EnvironmentName}.json",
                            optional: true);

                    })
                    .UseSerilog((hostingContext, loggerConfiguration) =>
                    {
                        loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
                    })
                    .ConfigureServices(services => services.AddAutofac());
                });
    }
}
