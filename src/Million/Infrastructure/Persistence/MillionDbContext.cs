using Million.Domain;

namespace Million.Infrastructure.Persistence;

public class MillionDbContext : DbContext
{
    public MillionDbContext(DbContextOptions<MillionDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; } = default!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MillionDbContext).Assembly);
    }
}
