namespace LeekLog.Abstractions.Models;

public class UserCreateModel
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string PasswordRepeat { get; set; } = string.Empty;
}
