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
    private readonly LeekLogDbContext _dbContext;

    public DbMigrator(
        ILogger<DbMigrator> logger,
        LeekLogDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task MigrateAsync()
    {
        IEnumerable<string> appliedMigrations = await _dbContext.Database.GetAppliedMigrationsAsync();

        _logger.LogInformation("Applied migrations: {appliedMigrations}", appliedMigrations.DefaultIfEmpty("<None>").ToArray());

        IEnumerable<string> pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();

        _logger.LogInformation("Pending migrations: {appliedMigrations}", pendingMigrations.DefaultIfEmpty("<None>").ToArray());

        await _dbContext.Database.MigrateAsync();

        _logger.LogInformation("Migrated database successfully");
    }
}
