using FluentValidation;
using GameModel;

namespace GameModel{
public class AnimalCreateDtoValidator : AbstractValidator<AlpacaCreateDto>
{
    public AnimalCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name must be 50 characters or less.");

        RuleFor(x => x.ImgUrl)
            .MaximumLength(255).WithMessage("Name must be 255 characters or less.");

        RuleFor(x => x.Gender)
            .IsInEnum();

        RuleFor(x => x.Capacity)
            .InclusiveBetween(130, 151).WithMessage("Capacity must be between 130 and 151.");

        RuleFor(x => x.Age)
            .GreaterThanOrEqualTo(0).WithMessage("Age cannot be negative.")
            .LessThanOrEqualTo(25).WithMessage("Age cannot be over 25");

        RuleFor(x => x.Energy)
            .InclusiveBetween(0, 100).WithMessage("Energy must be between 0 and 100.");

        RuleFor(x => x.Relationship)
            .InclusiveBetween(0, 100).WithMessage("Relationship must be between 0 and 100.");

        RuleFor(x => x.OwnerId)
            .Must(id => !id.HasValue || id.Value != Guid.Empty)
            .WithMessage("OwnerId must be a valid GUID if provided.");

    }
}
}
