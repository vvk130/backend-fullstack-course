using FluentValidation;

public class QuestionCreateDtoValidator : AbstractValidator<QuestionCreateDto>
{
    public QuestionCreateDtoValidator()
    {
        RuleFor(x => x.QuestionSentence)
            .NotEmpty()
            .WithMessage("Question sentence must not be empty.")
            .Must(q => q.Trim().EndsWith("?"))
            .WithMessage("Question must end with a question mark ('?').");

        RuleFor(x => x.Options)
            .NotNull()
            .WithMessage("Options are required.")
            .Must(options => options.Count >= 2)
            .WithMessage("At least two options are required.")
            .Must(options => options.Count(o => o.IsRightAnswer) == 1)
            .WithMessage("Exactly one option must be marked as correct.");

        RuleForEach(x => x.Options)
            .SetValidator(new OptionDtoValidator());

        RuleFor(x => x.Difficulty)
            .InclusiveBetween(1, 3)
            .WithMessage("Difficulty must be between 1 (Easy) and 3 (Hard).");
    }
}
