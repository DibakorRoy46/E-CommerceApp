using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }
    public DbSet<ProductHierarchy> ProductHierarchies => Set<ProductHierarchy>();
    public DbSet<Brand> Brands => Set<Brand>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductHierarchyConfiguration());
        modelBuilder.ApplyConfiguration(new ProductHierarchyConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}