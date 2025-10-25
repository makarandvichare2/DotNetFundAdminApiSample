namespace FundAdministration.API.Better.Tests
{
    public class FundControllerBetterMockedTest : IClassFixture<SqlContainerFixture>
    {
        private readonly SqlContainerFixture _fixture;
        private readonly CustomApiFactory _factory;
        private readonly HttpClient _client;

        public FundControllerBetterMockedTest(SqlContainerFixture fixture)
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
    }
}