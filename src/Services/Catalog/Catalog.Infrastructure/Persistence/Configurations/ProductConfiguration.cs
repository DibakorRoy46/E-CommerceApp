

using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(nameof(Product));
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(p => p.UnitId)
            .IsRequired();

        builder.Property(p => p.Weight)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.ImageUrl)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.BarCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.StockQuantity)
            .IsRequired();

        builder.HasOne(p => p.Brand)
            .WithMany()
            .HasForeignKey(p => p.BrandId)
            .OnDelete(DeleteBehavior.Restrict);


        // Configure other properties and relationships as needed

        builder.HasIndex(p => p.Code)
            .IsUnique();

        builder.HasIndex(p => p.BarCode)
            .IsUnique();

        builder.HasIndex(p => p.Name)
            .IsUnique();

    }
}
