using GameModel;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using FluentValidation;

public class SalesAdRequestValidator : AbstractValidator<SalesAdRequest>
{
    public SalesAdRequestValidator()
    {
        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be a positive integer.");

        RuleFor(x => x.EndTime)
            .GreaterThan(DateTime.UtcNow).WithMessage("End time must be in the future.")
            .Must((endTime) => endTime <= DateTime.UtcNow.AddDays(7))
            .WithMessage("End time must be within 7 days from today");
    }

}
