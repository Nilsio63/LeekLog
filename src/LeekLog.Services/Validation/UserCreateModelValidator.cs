using FluentValidation;
using LeekLog.Abstractions.Models;
using LeekLog.Data.Abstractions.Stores;

namespace LeekLog.Services.Validation;

public class UserCreateModelValidator : BaseValidator<UserCreateModel>
{
    private readonly List<char> _specialCharacters = new() { '@', '#', '!', '~', '$', '%', '^', '&', '*', '(', ')', '-', '+', '/', ':', '.', ',', '<', '>', '?', '|' };

    public UserCreateModelValidator(IUserStore userStore)
    {
        RuleFor(o => o.UserName)
            .NotEmpty()
            .Must(userName => userName.Any(o => char.IsWhiteSpace(o)) == false).WithMessage("User name must not contain white space characters")
            .MustAsync(async (userName, ct) => await userStore.GetByLoginAsync(userName, ct) is null).WithMessage(o => $"User name '{o.UserName}' is already taken");

        RuleFor(o => o.Password)
            .NotEmpty()
            .MinimumLength(8)
            .Must(password => password.Any(char.IsDigit)).WithMessage("Password must contain digits")
            .Must(password => password.Any(char.IsLower) && password.Any(char.IsUpper)).WithMessage("Password must contain upper and lower case letters")
            .Must(password => password.Any(_specialCharacters.Contains)).WithMessage($"Password must contain special characters ({string.Join("", _specialCharacters)})");

        RuleFor(o => o.PasswordRepeat)
            .NotEmpty()
            .Equal(o => o.Password).WithMessage("Password repetition must equal the password");
    }
}
