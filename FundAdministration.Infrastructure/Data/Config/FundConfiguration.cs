using FundAdministration.Core.Funds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FundAdministration.Infrastructure.Data.Config;

public class FundConfiguration : IEntityTypeConfiguration<Fund>
{
  public void Configure(EntityTypeBuilder<Fund> builder)
  {
        builder.ToTable("Funds");
        builder.HasKey(p=>p.Id);
        builder.OwnsOne(p => p.Currency, p =>
        {
            p.Property(pp => pp.CurrencyCode)
            .HasMaxLength(DataSchemaConstants.LENGTH_3)
            .IsRequired()
            .HasColumnName("CurrencyCode");
        });

        builder.Property(p => p.FundName)
        .HasMaxLength(DataSchemaConstants.LENGTH_100)
        .IsRequired();

        builder.HasMany(p => p.Investors)
               .WithOne(p => p.Fund)
               .HasForeignKey(p => p.FundId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
