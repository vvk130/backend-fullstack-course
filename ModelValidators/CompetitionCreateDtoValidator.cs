using FluentValidation;

public class CompetitionCreateDtoValidator : AbstractValidator<CompetitionCreateDto>
{
    public CompetitionCreateDtoValidator()
    {
        RuleFor(x => x.CompetitionType)
            .IsInEnum()
            .WithMessage("Invalid CompetitionType.");

        RuleFor(x => x.DaysToStart)
            .GreaterThanOrEqualTo(0)
            .WithMessage("DaysToStart must be 0 or greater.");

        RuleFor(x => x.DaysToEnd)
            .GreaterThan(1)
            .WithMessage("DaysToEnd must be greater than 1.");

        RuleFor(x => x)
            .Must(x => x.DaysToStart < x.DaysToEnd)
            .WithMessage("DaysToStart must be less than DaysToEnd.");
    }
}
