
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.ComponentModel.DataAnnotations;
using Testcontainers.MsSql;

namespace FundAdministration.API.Better.Tests
{
    public class FundControllerBetterMockedTest2 : IClassFixture<WebApplicationFactory<Program>>, IAsyncLifetime
    {
        private readonly MsSqlContainer _dbContainer;
        private readonly WebApplicationFactory<Program> _factory;
        private HttpClient _client;
        public FundControllerBetterMockedTest2(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _dbContainer = new MsSqlBuilder()
                .WithPassword("Mak123")
                .Build();
        }
        public async Task DisposeAsync()
        {
            await _dbContainer.StopAsync();
            _client.Dispose();
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();
            _client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((ctx, config) =>
                {
                    // Override DB connection string
                    Environment.SetEnvironmentVariable("ConnectionStrings__DefaultConnection",
                        _dbContainer.GetConnectionString());
                });
            }).CreateClient();
        }

        [Fact]
        public async Task HealthEndpoint_ReturnsOk()
        {
            var response = await _client.GetAsync("/health");
            response.EnsureSuccessStatusCode();
        }
    }
}