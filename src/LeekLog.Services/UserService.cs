using LeekLog.Abstractions.Entites;
using LeekLog.Data.Abstractions.Stores;
using LeekLog.Services.Abstractions;
using LeekLog.Services.Security;

namespace LeekLog.Services;

public class UserService : IUserService
{
    private readonly IUserStore _userStore;
    private readonly IPasswordEncoder _passwordEncoder;

    public UserService(
        IUserStore userStore,
        IPasswordEncoder passwordEncoder)
    {
        _userStore = userStore;
        _passwordEncoder = passwordEncoder;
    }

    public async Task<UserEntity?> TrySaveUserAsync(string userName, string password, CancellationToken ct = default)
    {
        UserEntity? existingUser = await _userStore.GetByLoginAsync(userName, ct);

        if (existingUser is not null)
        {
            return null;
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            return null;
        }

        byte[] encodedPassword = _passwordEncoder.EncodePassword(password);

        UserEntity newUser = new()
        {
            UserName = userName,
            Password = encodedPassword
        };

        await _userStore.SaveAsync(newUser, ct);

        return newUser;
    }
}
