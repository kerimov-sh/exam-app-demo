using FluentValidation;

namespace DemoApp.Business.Extensions;

public static class AbstractValidatorExtension
{
    public static ServiceResult? ValidateAsServiceResult<T>(this AbstractValidator<T> validator, T obj)
    {
        var validatorResult = validator.Validate(obj);

        if (validatorResult.Errors.Any())
        {
            return new ServiceResult(ResultState.ValidationFail,
                validatorResult.Errors.Select(err => err.ErrorMessage).ToArray());
        }

        return null;
    }
}