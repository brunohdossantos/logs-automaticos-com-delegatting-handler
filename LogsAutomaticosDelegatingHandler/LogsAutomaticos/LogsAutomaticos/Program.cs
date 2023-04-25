using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Security.Claims;
using System.Text;

namespace LogsAutomaticos
{
    public class Program
    {
        protected Program() { }

        public static async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            var cepService = host.Services.GetService<ICepService>();

            var response = await cepService.GetCepAsync("37900084");

            Console.ReadKey();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
              Host.CreateDefaultBuilder(args)
              .ConfigureAppConfiguration(app =>
              {
                  app.AddJsonFile("appsettings.json");
                  app.AddEnvironmentVariables();
              })
             .ConfigureServices((_, services) =>
             {
                 services
                    .AddTransient<LogDelegatingHandler>();

                 services
                    .AddTransient<ICepService, CepService>()
                    .AddHttpClient("cepService")
                    .AddHttpMessageHandler<LogDelegatingHandler>();
             })
             .UseSerilog((builder, configuration) =>
             {
                 configuration.ReadFrom.Configuration(builder.Configuration);
             });
    }

}