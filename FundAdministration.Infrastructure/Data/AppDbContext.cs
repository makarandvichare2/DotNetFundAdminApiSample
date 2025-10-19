using Ardalis.SharedKernel;
using FundAdministration.Core.Base;
using FundAdministration.Core.Funds;
using FundAdministration.Core.Investors;
using FundAdministration.Core.Transactions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FundAdministration.Infrastructure.Data;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    //private readonly IDomainEventDispatcher? _dispatcher = dispatcher;
    public DbSet<Fund> Funds => Set<Fund>();
    public DbSet<Investor> Investors => Set<Investor>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }

  //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
  //{
  //  int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

  //  if (_dispatcher == null) return result;

  //  var entitiesWithEvents = ChangeTracker.Entries<EntityBase>()
  //      .Select(e => e.Entity)
  //      .Where(e => e.DomainEvents.Any())
  //      .ToArray();

  //  await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

  //  return result;
  //}

  //public override int SaveChanges() =>
  //      SaveChangesAsync().GetAwaiter().GetResult();
}
