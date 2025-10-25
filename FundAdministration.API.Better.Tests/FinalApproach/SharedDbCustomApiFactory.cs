using FundAdministration.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Respawn;
using Testcontainers.MsSql;

namespace FundAdministration.API.Better.Tests.FinalApproach
{
    public class SharedDbCustomApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly MsSqlContainer _dbContainer = new MsSqlBuilder()
               .WithImage("mcr.microsoft.com/azure-sql-edge")
               .WithPassword("YourStrong(!)Password123")
               .WithEnvironment("ACCEPT_EULA", "Y")
               .WithCleanUp(true)
               .Build();
        public HttpClient HttpClient { get; private set; } = default!;

        public async Task ResetDataBaseAsync()
        {
            await _respawner.ResetAsync(_dbContainer.GetConnectionString());
        }

        // private DbConnection _connection = default!;
        private Respawner _respawner = default!;
        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();
            //_connection = new SqlConnection(_dbContainer.GetConnectionString());
            HttpClient = CreateClient();
            _respawner = await Respawner.CreateAsync(_dbContainer.GetConnectionString(),
               new RespawnerOptions
               {
                   DbAdapter = DbAdapter.SqlServer
               });
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                // 1. Remove the existing DbContext configuration (e.g., In-memory or real)
                //var dbContextDescriptor = services.SingleOrDefault(
                //    d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
                //if (dbContextDescriptor != null) services.Remove(dbContextDescriptor);
                services.RemoveAll<DbContextOptions<AppDbContext>>();
                // 2. Add the DbContext configuration using the container's connection string
                services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(_dbContainer.GetConnectionString()) // or UseSqlServer, etc.
                );
            });
        }

        public new async Task DisposeAsync()
        {
             //await _dbContainer.StopAsync();
            await _respawner.ResetAsync(_dbContainer.GetConnectionString());
        }
    }
}
