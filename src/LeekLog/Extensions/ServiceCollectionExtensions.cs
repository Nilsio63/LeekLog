using LeekLog.Authentication;
using Microsoft.AspNetCore.Components.Authorization;

namespace LeekLog.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<IUserAuthService, UserAuthService>();

        services.AddScoped<LeekLogAuthenticationStateProvider>();
        services.AddScoped<ILeekLogAuthenticationStateProvider>(s => s.GetRequiredService<LeekLogAuthenticationStateProvider>());
        services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<LeekLogAuthenticationStateProvider>());

        return services;
    }
}
