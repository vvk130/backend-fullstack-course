using FluentValidation;
using GameModel;
using System.Collections.Generic;

public class CompetitionRequestValidator : AbstractValidator<CompetitionRequest>
{
    public CompetitionRequestValidator()
    {
        RuleFor(x => x.CompetitionId)
            .NotEmpty().WithMessage("CompetitionId must not be empty.");

        RuleFor(x => x.HorseIds)
            .NotNull().WithMessage("HorseIds list cannot be null.")
            .Must(horses => horses.Count >= 2 && horses.Count <= 10)
            .WithMessage("You must provide between 3 and 10 horse IDs.");

        RuleForEach(x => x.HorseIds)
            .NotEmpty().WithMessage("Horse ID must not be empty GUID.");
    }
}
