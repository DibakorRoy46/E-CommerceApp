
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Entities;

namespace Ordering.Insfrastrueture.Presistence.Configurations;

public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("OutboxMessages");
        builder.HasKey(o => o.Id);
        builder.HasIndex(o => o.CorrelationId);
        builder.Property(o => o.Type).IsRequired().HasMaxLength(250);
        builder.Property(o => o.Content).IsRequired();
        builder.Property(o => o.OccurredOn).IsRequired();
        builder.Property(o => o.IsProcessed).IsRequired(false);
        builder.Ignore(x => x.IsProcessed);
    }
}
