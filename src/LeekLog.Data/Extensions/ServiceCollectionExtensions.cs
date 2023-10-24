using LeekLog.Data.Abstractions.Stores;
using LeekLog.Data.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LeekLog.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDbServices(this IServiceCollection services, Action<DbContextOptionsBuilder> buildOptions)
    {
        services.AddDbContextFactory<LeekLogDbContext>(o =>
        {
            buildOptions.Invoke(o);
        });

        services.AddTransient<IDbMigrator, DbMigrator>();
        services.AddTransient<IUserStore, UserStore>();

        return services;
    }
}
