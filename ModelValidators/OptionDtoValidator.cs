using FluentValidation;

public class OptionDtoValidator : AbstractValidator<OptionDto>
{
    public OptionDtoValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty()
            .WithMessage("Option text must not be empty.");
    }
}
