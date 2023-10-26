using LeekLog.Abstractions.Entites;
using System.Diagnostics.CodeAnalysis;

namespace LeekLog.Abstractions.Models;

public class UserLoginResult
{
    public UserEntity? User { get; }
    public string? ErrorMessage { get; }

    [MemberNotNullWhen(true, nameof(User))]
    [MemberNotNullWhen(false, nameof(ErrorMessage))]
    public bool Success { get; }

    public UserLoginResult(UserEntity createdUser)
    {
        User = createdUser;
        Success = true;
    }

    public UserLoginResult(string errorMessage)
    {
        ErrorMessage = errorMessage;
        Success = false;
    }
}
