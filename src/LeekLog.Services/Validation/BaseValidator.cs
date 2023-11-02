using FluentValidation;
using FluentValidation.Results;

namespace LeekLog.Services.Validation;

public abstract class BaseValidator<T> : AbstractValidator<T>, Abstractions.Validation.IValidator<T>
{
    public Func<object, string, Task<IEnumerable<string>>> ValidateValueFunction => async (model, propertyName) =>
    {
        ValidationResult result = await ValidateAsync(ValidationContext<T>.CreateWithOptions((T)model, o => o.IncludeProperties(propertyName)));

        if (result.IsValid)
        {
            return Array.Empty<string>();
        }

        return result.Errors.Select(o => o.ErrorMessage);
    };
}
