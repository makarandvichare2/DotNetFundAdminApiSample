using FundAdministration.API.Controllers.Funds;
using FundAdministration.Common.Funds;
using System.Net;

namespace FundAdministration.API.Better.Tests.FinalApproach
{

    [Collection("BetterApproach")]
    public class FundControllerBetterMockedTest : IAsyncLifetime
    {
        private readonly SharedDbCustomApiFactory _factory;

        public FundControllerBetterMockedTest(SharedDbCustomApiFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task HealthEndpoint_ReturnsOk()
        {
            var response = await _factory.HttpClient.GetAsync("/healthChecks");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetListAsync_ReturnsOk()
        {
            //Arrange
            var apiEndPoint = "/api/v1/Fund";
            _ = await _factory.HttpClient.PostAsJsonAsync(apiEndPoint,
                new CreateFundRequest("Fund 1", "EUR", DateTime.Now));

            _ = await _factory.HttpClient.PostAsJsonAsync(apiEndPoint, 
                new CreateFundRequest("Fund 2", "EUR", DateTime.Now));

            _ = await _factory.HttpClient.PostAsJsonAsync(apiEndPoint,
                new CreateFundRequest("Fund 3", "EUR", DateTime.Now));

            //Act Assert
            var funds = await _factory.HttpClient.GetFromJsonAsync<List<FundListDTO>>(apiEndPoint);
           // Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

           // var funds = await response.Content.ReadFromJsonAsync<List<FundListDTO>>();

            Assert.NotNull(funds);
            Assert.Equal(3, funds.Count);

            var testFund = funds.FirstOrDefault(f => f.fundName == "Fund 1");
            Assert.NotNull(testFund);
            Assert.Equal("EUR", testFund.currencyCode);
        }

        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        public Task DisposeAsync()
        {
            return _factory.ResetDataBaseAsync();
        }
    }
}