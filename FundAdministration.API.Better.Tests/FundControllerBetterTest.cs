
using Testcontainers.MsSql;

namespace FundAdministration.API.Better.Tests
{
    public class FundControllerBetterTest : IAsyncLifetime
    {
        private readonly MsSqlContainer _dbContainer;
        public FundControllerBetterTest()
        {
            _dbContainer = new MsSqlBuilder()
                .WithPassword("Mak123")
                .Build();
        }
        public async Task DisposeAsync()
        {
            await _dbContainer.StopAsync();
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();
        }

        [Fact]
        public async Task Should_Run_Query_On_Temporary_Database()
        {
            var connectionString = _dbContainer.GetConnectionString();

            using var conn = new Microsoft.Data.SqlClient.SqlConnection(connectionString);
            await conn.OpenAsync();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT 1";
            var result = await cmd.ExecuteScalarAsync();

            Assert.Equal(1, result);
        }
    }
}