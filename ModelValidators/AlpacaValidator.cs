using FluentValidation;
using GameModel;

public class AlpacaValidator : AbstractValidator<Alpaca>
{
    public AlpacaValidator()
    {
        Include(new AnimalValidator());

        RuleFor(x => x.AlpacaBreed)
            .IsInEnum().WithMessage("Breed must be a valid Alpaca breed.");

        RuleFor(x => x.AlpacaColor)
            .IsInEnum().WithMessage("Breed must be a valid Alpaca color.");
        
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

