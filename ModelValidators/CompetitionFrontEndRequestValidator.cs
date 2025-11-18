using FluentValidation;

public class CompetitionFrontEndRequestValidator : AbstractValidator<CompetitionFrontEndRequest>
{
    public CompetitionFrontEndRequestValidator()
    {
        RuleFor(x => x.CompetitionId)
            .NotEmpty().WithMessage("CompetitionId must not be empty.");

        RuleFor(x => x.HorseId1)
            .NotEmpty().WithMessage("HorseId1 must not be an empty GUID.");

        RuleFor(x => x.HorseId2)
            .NotEmpty().WithMessage("HorseId2 must not be an empty GUID.");

        RuleFor(x => x.HorseId3)
            .NotEmpty().WithMessage("HorseId3 must not be an empty GUID.");

        RuleFor(x => new[] { x.HorseId1, x.HorseId2, x.HorseId3 })
            .Must(ids => ids.Distinct().Count() == ids.Length)
            .WithMessage("Horse IDs must be unique.");
    }
}
