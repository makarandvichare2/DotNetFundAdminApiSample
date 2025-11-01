using FundAdministration.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FundAdministration.API.Helpers
{
    public class DBHelpers
    {
        public static async Task SeedDatabase(WebApplication app, IConfiguration config)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var useMigration = config.GetValue<bool>("UseMigration");
            try
            {
                var context = services.GetRequiredService<AppDbContext>();
                if (!useMigration)
                {
                    context.Database.EnsureCreated(); // if you want to create a db from scrach or changes automatically
                }
                {
                    context.Database.Migrate();// enable if you want to use migration
                }

                await SeedData.InitializeAsync(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
            }
        }

    }
}
