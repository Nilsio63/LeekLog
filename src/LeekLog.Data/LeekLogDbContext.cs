using Microsoft.EntityFrameworkCore;

namespace LeekLog.Data;

public class LeekLogDbContext : DbContext
{
    public LeekLogDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
