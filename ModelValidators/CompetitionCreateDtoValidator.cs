using FluentValidation;

public class CompetitionCreateDtoValidator : AbstractValidator<CompetitionCreateDto>
{
    public CompetitionCreateDtoValidator()
    {
        RuleFor(x => x.CompetitionType)
            .IsInEnum().WithMessage("Invalid CompetitionType");

        RuleFor(x => x.StartTime)
            .LessThan(x => x.EndTime)
            .WithMessage("Start time must be before end time.");

        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime)
            .WithMessage("End time must be after start time.");
    }
}
