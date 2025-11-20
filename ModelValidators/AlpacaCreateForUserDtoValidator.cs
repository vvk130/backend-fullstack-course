using FluentValidation;

public class AlpacaCreateForUserDtoValidator : AbstractValidator <AlpacaCreateForUserDto>
{
    public AlpacaCreateForUserDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("User Id cannot be empty.");

        RuleFor(x => x.AlpacaBreed)
            .IsInEnum()
            .When(x => x.AlpacaBreed.HasValue)
            .WithMessage("Breed is not valid.");
    }
}
