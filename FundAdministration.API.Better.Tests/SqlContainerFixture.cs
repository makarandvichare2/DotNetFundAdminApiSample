using Testcontainers.MsSql;

namespace FundAdministration.API.Better.Tests
{
    public sealed class SqlContainerFixture : IAsyncLifetime
    {
        private readonly MsSqlContainer _dbContainer = new MsSqlBuilder()
                .WithImage("mcr.microsoft.com/azure-sql-edge")
                .WithPassword("YourStrong(!)Password123")
                .WithEnvironment("ACCEPT_EULA", "Y")
                .WithCleanUp(true)
                .Build();

        public string ConnectionString => _dbContainer.GetConnectionString();

        public Task InitializeAsync() => _dbContainer.StartAsync();

        public Task DisposeAsync() => _dbContainer.DisposeAsync().AsTask();
    }
}
