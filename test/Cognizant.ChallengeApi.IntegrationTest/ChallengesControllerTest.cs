using System.Net.Http;
using System.Threading.Tasks;

using Autofac.Extensions.DependencyInjection;
using Cognizant.ChallangesApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

using Xunit;

namespace Cognizant.ChallangesApi.IntegrationTest
{
    public class ChallengesControllerrTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public ChallengesControllerrTest()
        {
            var webHostBuilder = new WebHostBuilder()
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    var env = builderContext.HostingEnvironment;
                    config.AddJsonFile("autofac.json")
                    .AddEnvironmentVariables();
                })
                .ConfigureServices(services => services.AddAutofac());

            _server = new TestServer(webHostBuilder);
            _client = _server.CreateClient();
        }

        [Trait("Cattegory","Integration")]
        [Fact]
        public async Task PostSolutionAsyncTest()
        {
            //var response = _client.PostAsync()...
            //too long...
        }
    }
}

