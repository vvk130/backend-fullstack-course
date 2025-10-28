using FluentValidation;
using GameModel;

public class AnimalValidator : AbstractValidator<Animal>
{
    public AnimalValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name must be 50 characters or less.");

        RuleFor(x => x.Gender)
            .IsInEnum();

        RuleFor(x => x.Capacity)
            .GreaterThanOrEqualTo(130)
            .LessThanOrEqualTo(151);

        RuleFor(x => x.Age)
            .GreaterThanOrEqualTo(0).WithMessage("Age cannot be negative.")
            .LessThanOrEqualTo(25).WithMessage("Age seems too high.");

        RuleFor(x => x.Energy)
            .InclusiveBetween(0, 100).WithMessage("Energy must be between 0 and 100.");

        RuleFor(x => x.Capacity)
            .InclusiveBetween(0, 10);

        RuleFor(x => x.Relationship)
            .InclusiveBetween(0, 100).WithMessage("Relationship must be between 0 and 100.");
    }
}
