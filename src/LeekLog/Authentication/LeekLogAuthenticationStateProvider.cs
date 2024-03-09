using Blazored.LocalStorage;
using LeekLog.Abstractions.Entites;
using LeekLog.Data.Abstractions.Stores;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace LeekLog.Authentication;

public interface ILeekLogAuthenticationStateProvider
{
    Task<AuthenticationState> GetAuthenticationStateAsync();
    Task RemoveAuthenticatedAsync(CancellationToken ct = default);
    Task SetAuthenticatedAsync(UserEntity user, CancellationToken ct = default);
}

public class LeekLogAuthenticationStateProvider : AuthenticationStateProvider, ILeekLogAuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorageService;
    private readonly IUserStore _userStore;

    public LeekLogAuthenticationStateProvider(
        ILocalStorageService localStorageService,
        IUserStore userStore)
    {
        _localStorageService = localStorageService;
        _userStore = userStore;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string? userId = await _localStorageService.GetItemAsync<string>(AuthConstants.LocalStorageUserIdKey);

        UserEntity? user = string.IsNullOrWhiteSpace(userId) ? null : await _userStore.GetByIdAsync(userId);

        ClaimsIdentity claimsIdentity = user != null
            ? GetClaimsIdentity(user)
            : new();

        ClaimsPrincipal claimsPrincipal = new(claimsIdentity);

        return new AuthenticationState(claimsPrincipal);
    }

    public async Task SetAuthenticatedAsync(UserEntity user, CancellationToken ct = default)
    {
        await _localStorageService.SetItemAsync(AuthConstants.LocalStorageUserIdKey, user.Id, ct);

        ClaimsIdentity identity = GetClaimsIdentity(user);

        ClaimsPrincipal claimsPrincipal = new(identity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }

    public async Task RemoveAuthenticatedAsync(CancellationToken ct = default)
    {
        await _localStorageService.RemoveItemAsync(AuthConstants.LocalStorageUserIdKey, ct);

        ClaimsIdentity identity = new();

        ClaimsPrincipal user = new(identity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    private static ClaimsIdentity GetClaimsIdentity(UserEntity user)
    {
        return new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "Normal")
        }, "apiauth_type");
    }
}
