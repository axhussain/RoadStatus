using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Text;

namespace RoadStatus
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: RoadStatus.exe RoadId");
                Console.WriteLine("Example: RoadStatus.exe A2");
                Environment.Exit(-1);
            }

            var roadId = args[0];

            // Create service collection
            var services = new ServiceCollection();
            ConfigureServices(services);

            // Create service provider
            var serviceProvider = services.BuildServiceProvider();

            // Instantiate the appropriate API Client via DI
            var api = serviceProvider.GetService<IApiClient>();
            var output = new StringBuilder();

            try
            {
                var result = api.GetRoadStatus(roadId).GetAwaiter().GetResult();

                foreach (var road in result)
                {
                    output.Append($"The status of the {road.DisplayName} is as follows")
                          .Append(Environment.NewLine)
                          .Append("\t")
                          .Append($"Road Status is {road.StatusSeverity}")
                          .Append(Environment.NewLine)
                          .Append("\t")
                          .Append($"Road Status Description is {road.StatusSeverityDescription}");
                    Console.WriteLine(output.ToString());
                }
            }
            catch
            {
                Console.WriteLine($"{roadId} is not a valid road");
                Environment.Exit(-1);
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Build config
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            // Map JSON config to AppSettings POCO class
            services.Configure<AppSettings>(configuration.GetSection("Tfl"));

            // Inject the appropriate API client
            services.AddTransient<IApiClient, ApiClient>();
        }
    }
}
