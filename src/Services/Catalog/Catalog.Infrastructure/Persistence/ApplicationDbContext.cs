

using Catalog.Application.Common.Interfaces;
using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
: DbContext(options), IApplicationDbContext
{
    public DbSet<Category> Categories => Set<Category>();
}
