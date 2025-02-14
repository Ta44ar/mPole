﻿using FluentValidation;
using Microsoft.Extensions.Localization;
using mPole.Data.Models;
using mPole.Resources;

public class PDClassValidator : AbstractValidator<Class>
{
    private readonly IStringLocalizer<SharedResource> _res;

    public PDClassValidator(IStringLocalizer<SharedResource> res)
    {
        _res = res;

        RuleFor(c => c.Trainer)
            .NotNull().WithMessage(_res["ValidationTrainerRequired"]);

        RuleFor(c => c.Date)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(_res["ValidationDateRequired"])
            .GreaterThanOrEqualTo(DateTime.Today).WithMessage(_res["ValidationDateInFuture"]);

        RuleFor(c => c.Time)
            .NotEmpty().WithMessage(_res["ValidationTimeRequired"]);

        RuleFor(c => c.Duration)
            .GreaterThan(TimeSpan.Zero).WithMessage(_res["ValidationDurationPositive"]);

        RuleFor(c => c.Location)
            .NotEmpty().WithMessage(_res["ValidationLocationRequired"]);

        RuleFor(c => c.Name)
            .NotEmpty().WithMessage(_res["ValidationNameRequired"]);
    }
}