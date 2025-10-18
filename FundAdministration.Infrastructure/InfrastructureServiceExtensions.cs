using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using FundAdministration.Infrastructure.Data;
using FundAdministration.Infrastructure.Data.Queries.Funds;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FundAdministration.Infrastructure;
public static class InfrastructureServiceExtensions
{
  public static IServiceCollection AddInfrastructureServices(
    this IServiceCollection services,
    ConfigurationManager config)
  {
    string? connectionString = config.GetConnectionString("SqlServerConnection");
    Guard.Against.Null(connectionString);
    services.AddDbContext<AppDbContext>(options =>
     options.UseSqlServer(connectionString));

    services.AddScoped(typeof(IEfRepository<>), typeof(EfRepository<>));
    services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
    services.AddScoped<IListFundQueryService, ListFundQueryService>();
    services.AddScoped<IGetFundQueryService, GetFundQueryService>();
        services.AddScoped<IListInvestorQueryService, ListInvestorQueryService>();
        services.AddScoped<IGetInvestorQueryService, GetInvestorQueryService>();

        return services;
  }
}
