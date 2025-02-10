using FinanceServices.Controllers;
using FinanceServices.Dto;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace FinanceServices
{
    public class IntegrationTests : IClassFixture<WebApplicationFactory<Simulation>>
    {
        private readonly HttpClient _client;

        public IntegrationTests(WebApplicationFactory<Simulation> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task EnsureSuccessOf_Simulation_GetCalculation()
        {
            string baseUrl = "/api/Simulation/getCalculation";

            var queryParams = new[]
            {
                ("initialAmount", "100"),
                ("mounthQuantity", "10")
            };

            var url = getUrlWithParams(baseUrl, queryParams);

            var response = await _client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<SimulationResultDto>(responseString);
            Assert.Equal([108.13m, 110.16m, 2.03m, 20.0m], [content?.LiquidAmount, content?.BruteAmount, content?.TributeAmount, content?.TributePercent]);
        }

        [Fact]
        public async Task EnsureFailureOf_ClientAccount_GetCalculation()
        {
            string baseUrl = "/api/Simulation/getCalculation";

            var queryParams = new[]
            {
                ("initialAmount", "0")
            };

            var url = getUrlWithParams(baseUrl, queryParams);

            var response = await _client.GetAsync(url);

            Assert.False(response.IsSuccessStatusCode);
        }

        static string getUrlWithParams(string baseUrl, (string, string)[] queryParams) =>
            baseUrl + "?" + string.Join("&", Array.ConvertAll(queryParams, param => $"{param.Item1}={param.Item2}"));
    }
}
