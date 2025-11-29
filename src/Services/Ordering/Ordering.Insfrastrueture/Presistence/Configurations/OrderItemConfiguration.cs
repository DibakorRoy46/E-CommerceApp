
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Entities;

namespace Ordering.Insfrastrueture.Presistence;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable(nameof(OrderItem));
        builder.HasKey(x=>x.OrderItemid);

        builder.Property(x=>x.OrderId)
            .IsRequired();

        builder.Property(x=>x.ProductId)
            .IsRequired();

        builder.Property(x=>x.ProductName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.ProductCode)
           .HasMaxLength(50)
           .IsRequired();

        builder.Property(x=>x.Quantity)
            .IsRequired();

        builder.Property(x => x.UnitPrice)
            .HasPrecision(18,2)
            .IsRequired();

        builder.Property(x => x.ItemWiseDicount)
            .HasPrecision(18, 2);


        // ⭐ Foreign key relationship configuration
        builder.HasOne<Order>()
            .WithMany(o => o.OrderItems)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
