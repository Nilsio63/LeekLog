using LeekLog.Services.Abstractions;
using LeekLog.Services.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LeekLog.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services, IConfiguration domainConfig)
    {
        services.AddOptions<SecuritySettings>().Bind(domainConfig.GetSection("Security"));

        services.AddTransient<IPasswordEncoder, PasswordEncoder>();

        services.AddTransient<IUserService, UserService>();

        return services;
    }
}
