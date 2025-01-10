using FluentValidation;
using Microsoft.Extensions.Localization;
using mPole.Data.Models;
using mPole.Resources;

public class MoveValidator : AbstractValidator<Move>
{
    private readonly IStringLocalizer<SharedResource> _res;

    public MoveValidator(IStringLocalizer<SharedResource> res)
    {
        _res = res;

        RuleFor(move => move.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(_res["ValidationNameRequired"])
            .MaximumLength(50).When(move => !string.IsNullOrEmpty(move.Name)).WithMessage(_res["ValidationNameTooLong"]);

        RuleFor(move => move.DifficultyLevel)
            .InclusiveBetween(1, 5).WithMessage(_res["ValidationDifficultyLevelRange"]);

        RuleFor(move => move.Description)
            .NotEmpty().WithMessage(_res["ValidationDescriptionRequired"]);

        RuleFor(move => move.Images)
            .NotEmpty().WithMessage(_res["ValidationOneImageAtLeast"]);
    }
}