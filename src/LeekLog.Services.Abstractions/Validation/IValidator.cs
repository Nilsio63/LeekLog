namespace LeekLog.Services.Abstractions.Validation;

public interface IValidator<T> : FluentValidation.IValidator<T>
{
    Func<object, string, Task<IEnumerable<string>>> ValidateValue { get; }
}
