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

        // RuleFor(x => x.Personalities)
        //     .NotNull().WithMessage("Personalities cannot be null.")
        //     .Must(p => p.Count == 3).WithMessage("There must be exactly 3 personalities.")
        //     .ForEach(personality => personality.SetValidator(new PersonalityTypeValidator()));

        RuleFor(x => x.OwnerId)
            .Must(id => !id.HasValue || id.Value != Guid.Empty)
            .WithMessage("OwnerId must be a valid GUID if provided.");

        // RuleFor(x => x.SireId)
        //     .Must(id => id == null)
        //     .WithMessage("SireId cannot be set manually.");

        // RuleFor(x => x.DamId)
        //     .Must(id => id == null)
        //     .WithMessage("DamId cannot be set manually.");

        
        // RuleFor(x => x.Contact).SetInheritanceValidator(v => 
        // {
        // v.Add<Alpaca>(new AlpacaValidator());
        // });

    }
}
}
// public class PersonalityTypeValidator : AbstractValidator<PersonalityType>
// {
//     public PersonalityTypeValidator()
//     {
//         RuleFor(x => x.PersonalityTrait).IsInEnum().WithMessage("Invalid PersonalityTrait.");
//         RuleFor(x => x.Severity).InclusiveBetween(0, 10).WithMessage("Severity must be between 0 and 10.");
//         RuleFor(x => x.Discovered).NotNull().WithMessage("Discovered is required.");
//     }
// }
// }