using LeekLog.Services.Abstractions;
using LeekLog.Services.Security;
using Microsoft.Extensions.DependencyInjection;

namespace LeekLog.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddTransient<IPasswordEncoder, PasswordEncoder>();

        services.AddTransient<IUserService, UserService>();

        return services;
    }
}
