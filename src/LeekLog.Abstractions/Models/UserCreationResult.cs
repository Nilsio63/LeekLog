using LeekLog.Abstractions.Entites;
using System.Diagnostics.CodeAnalysis;

namespace LeekLog.Abstractions.Models;

public class UserCreationResult
{
    public UserEntity? CreatedUser { get; }
    public string? ErrorMessage { get; }

    [MemberNotNullWhen(true, nameof(CreatedUser))]
    [MemberNotNullWhen(false, nameof(ErrorMessage))]
    public bool Success { get; }

    public UserCreationResult(UserEntity createdUser)
    {
        CreatedUser = createdUser;
        Success = true;
    }

    public UserCreationResult(string errorMessage)
    {
        ErrorMessage = errorMessage;
        Success = false;
    }
}
