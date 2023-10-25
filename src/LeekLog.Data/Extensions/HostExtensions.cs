using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LeekLog.Data.Extensions;

public static class HostExtensions
{
    public static async Task UseDatabaseAsync(this IHost host)
    {
        using IServiceScope scope = host.Services.CreateScope();

        IDbMigrator dbMigrator = scope.ServiceProvider.GetRequiredService<IDbMigrator>();

        await dbMigrator.MigrateAsync();

        IMetaDataInitializer metaDataInitializer = scope.ServiceProvider.GetRequiredService<IMetaDataInitializer>();

        await metaDataInitializer.InitMetaDataAsync();
    }
}
