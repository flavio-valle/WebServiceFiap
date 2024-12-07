using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace WebServiceFiap.Tests
{
    public class MonitoramentoAguaControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public MonitoramentoAguaControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_ShouldReturn200()
        {
            // Act
            var response = await _client.GetAsync("/api/monitoramentoagua?pageNumber=1&pageSize=5");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var json = await response.Content.ReadAsStringAsync();
            Assert.NotNull(json);
        }
    }
}
