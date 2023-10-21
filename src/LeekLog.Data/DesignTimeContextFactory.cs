using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LeekLog.Data;

internal class DesignTimeContextFactory : IDesignTimeDbContextFactory<LeekLogDbContext>
{
    public LeekLogDbContext CreateDbContext(string[] args)
    {
        string connectionString = "Server=localhost;Port=3306;Database=leeklog;User=root;Password=TestPassword123!";
        var optionsBuilder = new DbContextOptionsBuilder<LeekLogDbContext>();
        optionsBuilder.UseMySql(connectionString, ServerVersion.Parse("8.0.0"));

        return new LeekLogDbContext(optionsBuilder.Options);
    }
}
