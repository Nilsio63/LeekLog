﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LeekLog.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDbServices(this IServiceCollection services, Action<DbContextOptionsBuilder> buildOptions)
    {
        services.AddDbContext<LeekLogDbContext>(o =>
        {
            buildOptions.Invoke(o);
        });

        services.AddTransient<IDbMigrator, DbMigrator>();

        return services;
    }
}