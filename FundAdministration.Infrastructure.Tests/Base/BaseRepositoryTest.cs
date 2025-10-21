using FundAdministration.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FundAdministration.Infrastructure.Tests.Base;

public abstract class BaseRepositoryTest
{
    protected AppDbContext CreateContext(string databaseName)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: databaseName)
            .Options;

        return new AppDbContext(options);
    }
}