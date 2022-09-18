using Million.Categories.Domain;
using Million.Questions.Domain;

namespace Million.Shared.Infrastructure.Persistence;

public class MillionDbContext : DbContext
{
    public MillionDbContext(DbContextOptions<MillionDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; } = default!;
    public DbSet<Question> Questions { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MillionDbContext).Assembly);
    }
}
