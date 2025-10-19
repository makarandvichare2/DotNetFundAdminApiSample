using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using FundAdministration.Infrastructure.Data;
using FundAdministration.Infrastructure.Data.Queries.Funds;
using FundAdministration.Infrastructure.Data.Queries.Investors;
using FundAdministration.Infrastructure.Data.Queries.Transactions;
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

        //services.AddScoped(typeof(IReadRepository<>), typeof(NonDeletableRepository<>));
        services.AddScoped(typeof(INonDeletableRepository<>), typeof(NonDeletableRepository<>));

        services.AddScoped(typeof(ISoftDeleteRepository<>), typeof(SoftDeleteRepository<>));

        services.AddScoped<IListFundQueryService, ListFundQueryService>();
        services.AddScoped<IGetFundQueryService, GetFundQueryService>();

        services.AddScoped<IListInvestorQueryService, ListInvestorQueryService>();
        services.AddScoped<IGetInvestorQueryService, GetInvestorQueryService>();

        services.AddScoped<ITransactionByInvestorQueryService, TransactionByInvestorQueryService>();
        services.AddScoped<ITotalAmountGroupByFundQueryService, TotalAmountGroupByFundQueryService>();

        return services;
  }
}
