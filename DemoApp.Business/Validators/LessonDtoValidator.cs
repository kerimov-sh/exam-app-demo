using DemoApp.Business.DataTransferObjects;
using DemoApp.Business.Helpers;
using FluentValidation;

namespace DemoApp.Business.Validators;

public class LessonDtoValidator : AbstractValidator<LessonDto>
{
    public LessonDtoValidator()
    {
        RuleFor(x => x.Code)
            .Length(3)
            .WithMessage(ValidationMessages.GetStaticLengthMessage("Kodu", 3));

        RuleFor(x => x.Name)
            .MaximumLength(30)
            .WithMessage(ValidationMessages.GetMaximumLengthMessage("Adı", 30));

        RuleFor(x => x.Class)
            .InclusiveBetween(1, 12)
            .WithMessage(ValidationMessages.GetOutOfRangeMessage("Sinifi", 1, 12));

        RuleFor(x => x.TeacherName)
            .MaximumLength(20)
            .WithMessage(ValidationMessages.GetMaximumLengthMessage("Müəllimin adı", 20));

        RuleFor(x => x.TeacherSurname)
            .MaximumLength(20)
            .WithMessage(ValidationMessages.GetMaximumLengthMessage("Müəllimin soyadı", 20));
    }
}