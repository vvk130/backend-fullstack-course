using FluentValidation;
using GameModel;

public class LevelValidator : AbstractValidator<Level>
{
    public LevelValidator()
    {
        RuleFor(x => x.LevelNumber).GreaterThanOrEqualTo(0).WithMessage("LevelNumber must be 0 or positive.");
        RuleFor(x => x.EntryPoints).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Stable).NotNull().WithMessage("Stable is required.");

        RuleFor(x => x.Stable.ImgUrl).NotEmpty();
        RuleFor(x => x.Stable.Description).NotEmpty();
        RuleFor(x => x.Stable.StableAmount).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Stable.EnvironmentScore).InclusiveBetween(0, 1000);
        RuleFor(x => x.Stable.Cleanleness).InclusiveBetween(0, 3); 
    }
}
