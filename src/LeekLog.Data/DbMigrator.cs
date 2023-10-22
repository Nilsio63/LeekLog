using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LeekLog.Data;

public interface IDbMigrator
{
    Task MigrateAsync();
}

public class DbMigrator : IDbMigrator
{
    private readonly ILogger<DbMigrator> _logger;
    private readonly IDbContextFactory<LeekLogDbContext> _dbContextFactory;

    public DbMigrator(
        ILogger<DbMigrator> logger,
        IDbContextFactory<LeekLogDbContext> dbContextFactory)
    {
        _logger = logger;
        _dbContextFactory = dbContextFactory;
    }

    public async Task MigrateAsync()
    {
        using LeekLogDbContext context = await _dbContextFactory.CreateDbContextAsync();

        IEnumerable<string> appliedMigrations = await context.Database.GetAppliedMigrationsAsync();

        _logger.LogInformation("Applied migrations: {appliedMigrations}", appliedMigrations.DefaultIfEmpty("<None>").ToArray());

        IEnumerable<string> pendingMigrations = await context.Database.GetPendingMigrationsAsync();

        _logger.LogInformation("Pending migrations: {appliedMigrations}", pendingMigrations.DefaultIfEmpty("<None>").ToArray());

        await context.Database.MigrateAsync();

        _logger.LogInformation("Migrated database successfully");
    }
}
