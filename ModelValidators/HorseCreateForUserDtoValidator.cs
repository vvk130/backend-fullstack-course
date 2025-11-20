using FluentValidation;

public class HorseCreateForUserDtoValidator : AbstractValidator<HorseCreateForUserDto>
{
    public HorseCreateForUserDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("User Id cannot be empty.");

        RuleFor(x => x.Breed)
            .IsInEnum()
            .When(x => x.Breed.HasValue)
            .WithMessage("Breed is not valid.");
    }
}
