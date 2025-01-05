using FluentValidation;
using Microsoft.Extensions.Localization;
using mPole.Resources;

public class QuestionValidator : AbstractValidator<QuestionModel>
{
    private readonly IStringLocalizer<SharedResource> _res;

    public QuestionValidator(IStringLocalizer<SharedResource> res)
    {
        _res = res;

        RuleFor(question => question.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(_res["ValidationEmailRequired"])
            .EmailAddress().WithMessage(_res["ValidationEmailInvalid"]);

        RuleFor(question => question.Question)
            .NotEmpty().WithMessage(_res["ValidationQuestionRequired"]);
    }
}
