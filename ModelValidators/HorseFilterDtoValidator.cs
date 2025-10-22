using FluentValidation;
using System;
using System.Collections.Generic;

public class HorseFilterDtoValidator : AbstractValidator<HorseFilterDto>
{
    public HorseFilterDtoValidator()
    {
        RuleFor(x => x.Genders)
            .Must(genders => genders == null || genders.Count > 0)
            .WithMessage("Genders cannot be empty if provided.");

        RuleFor(x => x.Breeds)
            .Must(breeds => breeds == null || breeds.Count > 0)
            .WithMessage("Breeds cannot be empty if provided.");

        RuleFor(x => x.MinAge)
            .GreaterThanOrEqualTo(0)
            .When(x => x.MinAge.HasValue)
            .WithMessage("MinAge must be greater than or equal to 0.");

        RuleFor(x => x.MaxAge)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(25)
            .When(x => x.MaxAge.HasValue)
            .WithMessage("MaxAge must be between 0 and 25.");

        RuleFor(x => x.MinAge)
            .LessThanOrEqualTo(x => x.MaxAge)
            .When(x => x.MinAge.HasValue && x.MaxAge.HasValue)
            .WithMessage("MinAge cannot be greater than MaxAge.");
    }
}
