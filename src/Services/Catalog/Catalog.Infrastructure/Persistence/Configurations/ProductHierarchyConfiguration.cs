
using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.Configurations;

public class ProductHierarchyConfiguration :IEntityTypeConfiguration<ProductHierarchy>
{
    public void Configure(EntityTypeBuilder<ProductHierarchy> builder)
    {
        builder.ToTable("ProductHierarchies");
        builder.HasKey(x => x.Id);
        builder.Property(x =>x.Id).HasColumnName("id").UseIdentityAlwaysColumn();
        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(200)
               .HasColumnName("name");
        builder.Property(x => x.Code)
               .IsRequired()
               .HasMaxLength(50)
               .HasColumnName("code");
        builder.Property(x => x.Status)
               .IsRequired()
               .HasColumnName("status");
        builder.Property(x => x.CreatedDate)
               .IsRequired()
               .HasColumnName("createddate");
        builder.Property(x => x.CreatedBy)
               .HasMaxLength(100)
               .HasColumnName("createdby");
        builder.Property(x => x.ModifiedDate)
               .HasColumnName("modifieddate");
        builder.Property(x => x.ModifiedBy)
               .HasMaxLength(100)
               .HasColumnName("modifiedby");
        // Indexes
        builder.HasIndex(x => x.Code).IsUnique();
    }
}

