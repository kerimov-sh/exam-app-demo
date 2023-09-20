using DemoApp.Business.DataTransferObjects;
using DemoApp.Business.Helpers;
using FluentValidation;

namespace DemoApp.Business.Validators;

public class ExamDtoValidator : AbstractValidator<ExamDto>
{
    public ExamDtoValidator()
    {
        RuleFor(x => x.LessonCode)
            .MaximumLength(3)
            .WithMessage(ValidationMessages.GetMaximumLengthMessage("Dərsin kodu", 3));

        RuleFor(x => x.StudentNumber)
            .InclusiveBetween(1, 99999)
            .WithMessage(ValidationMessages.GetOutOfRangeMessage("Şagirdin nömrəsi", 1, 99999));

        RuleFor(x => x.Score)
            .InclusiveBetween(2, 5)
            .WithMessage(ValidationMessages.GetOutOfRangeMessage("Qiyməti", 2, 5));
    }
}