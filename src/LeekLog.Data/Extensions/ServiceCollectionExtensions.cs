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
        services.AddTransient<IMetaDataInitializer, MetaDataInitializer>();
        services.AddTransient<IUserStore, UserStore>();
        services.AddTransient<IExerciseStatisticsStore, ExerciseStatisticsStore>();
        services.AddTransient<IExerciseStore, ExerciseStore>();
        services.AddTransient<IFeedbackStore, FeedbackStore>();
        services.AddTransient<IGymSessionStore, GymSessionStore>();
        services.AddTransient<ISessionExerciseStore, SessionExerciseStore>();

        return services;
    }
}
