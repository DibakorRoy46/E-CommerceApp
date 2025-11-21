
using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.Configurations;

public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.ToTable(nameof(Brand));
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("name");

        builder.Property(b => b.Code)
            .HasColumnName("code")
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Status)
               .IsRequired()
               .HasColumnName("status");

        builder.Property(x => x.CreatedDate)
               .IsRequired()
               .HasColumnName("createddate");

        builder.Property(x => x.CreatedBy)
               .IsRequired()
               .HasMaxLength(100)
               .HasColumnName("createdby");

        builder.Property(x => x.ModifiedDate)
               .HasColumnName("modifieddate");

        builder.Property(x => x.ModifiedBy)
               .HasMaxLength(100)
               .HasColumnName("modifiedby");
        // Indexes
        builder.HasIndex(x => x.Code).IsUnique();
        builder.HasIndex(x => x.Name).IsUnique();
    }
}
