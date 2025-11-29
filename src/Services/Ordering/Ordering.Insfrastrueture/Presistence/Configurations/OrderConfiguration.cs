
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Entities;

namespace Ordering.Insfrastrueture.Presistence;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable(nameof(Order));
        builder.HasKey(x => x.OrderId);

        builder.Property(b => b.UserName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.GrossValue)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.DiscountValue)
            .HasPrecision(18, 2);  

        builder.Property(x => x.NetValue)
            .IsRequired()
            .HasPrecision(18, 2);  

        builder.Property(x => x.NumberOfItems)
            .IsRequired();

        builder.Property(x => x.FirstName)
            .HasMaxLength(100);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.EmailAddress)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.AddressLine)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Country)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.State)
            .HasMaxLength(100);

        builder.Property(x => x.ZipCode)
            .HasMaxLength(20);

        builder.Property(x => x.CardName)
            .HasMaxLength(100);

        builder.Property(x => x.CardNumber)
            .HasMaxLength(50);

        builder.Property(x => x.Expiration)
            .HasMaxLength(10); 

        builder.Property(x => x.Cvv)
            .HasMaxLength(10);

        builder.Property(x => x.PaymentMethod)
            .HasConversion<int>()   
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<int>()     
            .IsRequired();

        builder.Property(x => x.Remarks)
            .HasMaxLength(500);
    }
}

