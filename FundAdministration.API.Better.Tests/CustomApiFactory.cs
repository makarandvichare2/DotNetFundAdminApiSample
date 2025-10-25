using FundAdministration.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace FundAdministration.API.Better.Tests
{
    public class CustomApiFactory : WebApplicationFactory<Program>
    {
        private readonly string _connectionString;

        public CustomApiFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                // 1. Remove the existing DbContext configuration (e.g., In-memory or real)
                var dbContextDescriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
                if (dbContextDescriptor != null) services.Remove(dbContextDescriptor);

                // 2. Add the DbContext configuration using the container's connection string
                services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(_connectionString) // or UseSqlServer, etc.
                );
            }).UseEnvironment("Tests");
        }
    }
}
