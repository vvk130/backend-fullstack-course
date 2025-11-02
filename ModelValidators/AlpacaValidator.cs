using FluentValidation;
using GameModel;

public class AlpacaValidator : AnimalValidator
{
    public AlpacaValidator()
    {
        RuleFor(x => x.AlpacaBreed)
            .IsInEnum().WithMessage("Breed must be a valid Alpaca breed.");

        RuleFor(x => x.AlpacaColor)
            .IsInEnum().WithMessage("Breed must be a valid Alpaca color.");

        RuleFor(x => x.PersonalityTraits)
            .NotNull().WithMessage("PersonalityTraits cannot be null.")
            .Must(list => list.Count <= 5).WithMessage("At most 5 personality traits allowed.");

        RuleFor(x => x.Fears)
            .NotNull().WithMessage("Fears cannot be null.")
            .Must(list => list.All(f => f.Severity >= 0 && f.Severity <= 10))
            .WithMessage("Each fear severity must be between 0 and 10.");
    }
}
using FluentValidation;
using GameModel;

public class AlpacaValidator : AbstractValidator<Alpaca>
{
    public AlpacaValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name must be 50 characters or less.");

        RuleFor(x => x.Gender)
            .IsInEnum();

        RuleFor(x => x.Capacity)
            .InclusiveBetween(130, 151).WithMessage("Capacity must be between 130 and 151.");

        RuleFor(x => x.Age)
            .InclusiveBetween(0, 25).WithMessage("Age must be between 0 and 25.");

        RuleFor(x => x.Energy)
            .InclusiveBetween(0, 100).WithMessage("Energy must be between 0 and 100.");

        RuleFor(x => x.Relationship)
            .InclusiveBetween(0, 100).WithMessage("Relationship must be between 0 and 100.");

        RuleFor(x => x.AlpacaQualities).NotNull().WithMessage("AlpacaQualities is required.");
        RuleFor(x => x.AlpacaQualities).SetValidator(new AlpacaQualitiesValidator());

    }
}

public class AlpacaQualitiesValidator : AbstractValidator<AlpacaQualities>
{
    public AlpacaQualitiesValidator()
    {
        RuleFor(x => x.Agility).InclusiveBetween(0, 10);
        RuleFor(x => x.Speed).InclusiveBetween(0, 10);
        RuleFor(x => x.Intelligence).InclusiveBetween(0, 10);
        RuleFor(x => x.JumpingAbility).InclusiveBetween(0, 10);
        RuleFor(x => x.WoolQuality).InclusiveBetween(0, 10);
    }
}

