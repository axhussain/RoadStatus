using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoadStatus;
using System.IO;
using System.Net.Http;

namespace RoadStatusTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_MockApiResult()
        {
            var services = new ServiceCollection();
            services.AddTransient<IApiClient, MockApiClient>();
            var serviceProvider = services.BuildServiceProvider();
            var api = serviceProvider.GetService<IApiClient>();

            var result = api.GetRoadStatus("A2").GetAwaiter().GetResult();

            Assert.AreEqual(1, result.Count); // There should only be one RoadCorridor in the List
            Assert.AreEqual("A2", result[0].DisplayName);
            Assert.AreEqual("Good", result[0].StatusSeverity);
            Assert.AreEqual("No Exceptional Delays", result[0].StatusSeverityDescription);
        }

        [TestMethod]
        public void Test_ValidRoadResult()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();
            var api = serviceProvider.GetService<IApiClient>();

            var result = api.GetRoadStatus("a2").GetAwaiter().GetResult();

            Assert.AreEqual(1, result.Count); // There should only be one RoadCorridor in the List
            Assert.AreEqual("A2", result[0].DisplayName);
        }

        [TestMethod]
        public void Test_InvalidRoadResult()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();
            var api = serviceProvider.GetService<IApiClient>();

            Assert.ThrowsExceptionAsync<HttpRequestException>(async () =>
            {
                api.GetRoadStatus("a233").GetAwaiter().GetResult();
            });
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
