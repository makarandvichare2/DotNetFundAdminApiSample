using FundAdministration.Core.Investors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FundAdministration.Infrastructure.Data.Config;

public class InvestorConfiguration : IEntityTypeConfiguration<Investor>
{
  public void Configure(EntityTypeBuilder<Investor> builder)
  {
        builder.ToTable("Investors");
        builder.HasKey(x => x.Id);

        builder.OwnsOne(p => p.Email, p =>
        {
            p.Property(pp => pp.EmailId)
            .HasColumnName("EmailId");
        });

        builder.Property(p => p.FullName)
        .HasMaxLength(DataSchemaConstants.LENGTH_100)
        .IsRequired();

        builder.Property(i => i.FundId)
               .IsRequired();

        builder.HasMany(p => p.Transactions)
               .WithOne(p => p.Investor)
               .HasForeignKey(p => p.InvestorId)
               .OnDelete(DeleteBehavior.Cascade);

    }
}
