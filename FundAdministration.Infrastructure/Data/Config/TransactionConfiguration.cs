using FundAdministration.Core.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FundAdministration.Infrastructure.Data.Config;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
  public void Configure(EntityTypeBuilder<Transaction> builder)
  {

        builder.ToTable("Transactions");
        builder.HasKey(x => x.Id);

        builder.Property(p => p.Amount)
        .HasPrecision(18, 2)
        .IsRequired();

        builder.Property(i => i.InvestorId)
                   .IsRequired();
    }
}
