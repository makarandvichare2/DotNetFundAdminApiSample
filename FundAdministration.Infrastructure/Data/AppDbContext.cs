using FundAdministration.Core.Funds;
using FundAdministration.Core.Investors;
using FundAdministration.Core.Transactions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FundAdministration.Infrastructure.Data;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Fund> Funds => Set<Fund>();
    public DbSet<Investor> Investors => Set<Investor>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }

}
