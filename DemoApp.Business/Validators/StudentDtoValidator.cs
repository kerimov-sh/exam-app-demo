using DemoApp.Business.DataTransferObjects;
using DemoApp.Business.Helpers;
using FluentValidation;

namespace DemoApp.Business.Validators;

public class StudentDtoValidator : AbstractValidator<StudentDto>
{
    public StudentDtoValidator()
    {
        RuleFor(x => x.Number)
            .InclusiveBetween(1, 99999)
            .WithMessage(ValidationMessages.GetOutOfRangeMessage("Nömrəsi", 1, 99999));

        RuleFor(x => x.Name)
            .MaximumLength(30)
            .WithMessage(ValidationMessages.GetMaximumLengthMessage("Adı", 30));

        RuleFor(x => x.Surname)
            .MaximumLength(30)
            .WithMessage(ValidationMessages.GetMaximumLengthMessage("Soyadı", 30));

        RuleFor(x => x.Class)
            .InclusiveBetween(1, 12)
            .WithMessage(ValidationMessages.GetOutOfRangeMessage("Sinifi", 1, 12));
    }
}