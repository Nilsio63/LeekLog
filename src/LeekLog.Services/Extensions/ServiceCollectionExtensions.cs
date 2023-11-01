using LeekLog.Abstractions.Models;
using LeekLog.Services.Abstractions;
using LeekLog.Services.Abstractions.Validation;
using LeekLog.Services.Security;
using LeekLog.Services.Validation;
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
        services.AddTransient<IExerciseService, ExerciseService>();
        services.AddTransient<IGymSessionService, GymSessionService>();

        services.AddTransient<IValidator<UserCreateModel>, UserCreateModelValidator>();

        return services;
    }
}
