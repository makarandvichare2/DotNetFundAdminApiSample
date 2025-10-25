using FundAdministration.API.Better.Tests.FinalApproach;
using FundAdministration.Common.Funds;
using System.Net;

namespace FundAdministration.API.Better.Tests
{

    public class FundControllerBetterMockedTest3 : IClassFixture<SqlContainerFixture>
    {
        private readonly SqlContainerFixture _fixture;
        private readonly CustomApiFactory _factory;
        private readonly HttpClient _client;

        public FundControllerBetterMockedTest3(SqlContainerFixture fixture)
        {
            _fixture = fixture;
            _factory = new CustomApiFactory(_fixture.ConnectionString);
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task HealthEndpoint_ReturnsOk()
        {
            var response = await _client.GetAsync("/healthChecks");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetListAsync_ReturnsOk()
        {
            var response = await _client.GetAsync("/v1/Fund");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var funds = await response.Content.ReadFromJsonAsync<List<FundListDTO>>();

            Assert.NotNull(funds);
            Assert.Equal(3, funds.Count);

            var testFund = funds.FirstOrDefault(f => f.fundName == "Fund A");
            Assert.NotNull(testFund);
            Assert.Equal("EUR", testFund.currencyCode);
        }
    }
}