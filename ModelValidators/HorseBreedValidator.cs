using FluentValidation;
using GameModel;

public class HorseBreedValidator : AbstractValidator<HorseBreed>
{
    public HorseBreedValidator()
    {
        RuleFor(x => x.Breed)
            .IsInEnum();

        RuleFor(x => x.MinHeightCm)
            .GreaterThan(90);

        RuleFor(x => x.MaxHeightCm)
            .GreaterThan(x => x.MinHeightCm)
            .LessThanOrEqualTo(195);

        RuleFor(x => x.PossibleColors)
            .NotNull().WithMessage("PossibleColors must not be null.")
            .Must(colors => colors.All(c => Enum.IsDefined(typeof(Color), c)))
            .WithMessage("One or more colors are not valid.");

    }
}
