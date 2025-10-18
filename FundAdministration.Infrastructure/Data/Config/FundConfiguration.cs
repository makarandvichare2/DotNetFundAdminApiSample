using FundAdministration.Core.Funds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FundAdministration.Infrastructure.Data.Config;

public class FundConfiguration : IEntityTypeConfiguration<Fund>
{
  public void Configure(EntityTypeBuilder<Fund> builder)
  {

        builder.OwnsOne(p => p.Currency, p =>
        {
            p.Property(pp => pp.CurrencyCode)
            .HasColumnName("CurrencyCode");
        });

        builder.Property(p => p.FundName)
        .HasMaxLength(DataSchemaConstants.MAX_LENGTH_100)
        .IsRequired();

        builder.Property(p => p.Currency)
        .HasMaxLength(DataSchemaConstants.MAX_LENGTH_3)
        .IsRequired();
    }
}
