
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.ComponentModel.DataAnnotations;
using Testcontainers.MsSql;

namespace FundAdministration.API.Better.Tests
{
    public class FundControllerBetterMockedTest1 : IAsyncLifetime
    {
        private readonly MsSqlContainer _dbContainer;
        private HttpClient _client;
        public FundControllerBetterMockedTest1()
        {
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
            var appFactory = new WebApplicationFactory<Program>()
                   .WithWebHostBuilder(builder =>
                   {
                       builder.ConfigureAppConfiguration((context, config) =>
                       {
                           // Replace connection string in appsettings for testing
                           Environment.SetEnvironmentVariable(
                               "ConnectionStrings__SqlServerConnection",
                               _dbContainer.GetConnectionString());
                       });
                   });
            _client = appFactory.CreateClient();
        }

        [Fact]
        public async Task HealthEndpoint_ReturnsOk()
        {
            var response = await _client.GetAsync("/health");
            response.EnsureSuccessStatusCode();
        }
    }
}