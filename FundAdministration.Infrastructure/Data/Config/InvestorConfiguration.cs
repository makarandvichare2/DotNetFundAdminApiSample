using FundAdministration.Core.Investors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FundAdministration.Infrastructure.Data.Config;

public class InvestorConfiguration : IEntityTypeConfiguration<Investor>
{
  public void Configure(EntityTypeBuilder<Investor> builder)
  {
        builder.OwnsOne(p => p.Email, p =>
        {
            p.Property(pp => pp.EmailId)
            .HasColumnName("EmailId");
        });

        builder.Property(p => p.FullName)
        .HasMaxLength(DataSchemaConstants.MAX_LENGTH_100)
        .IsRequired();
  }
}
