using FluentValidation;
using GameModel;

public class AddStockImgRequestValidator : AbstractValidator<StockImgDto>
{
    private const string AllowedPrefix = "https://res.cloudinary.com/dn4bwpln0/";

    public AddStockImgRequestValidator()
    {
        RuleFor(x => x.imgUrl)
            .NotEmpty()
            .WithMessage("Image URL cannot be empty.")
            .Must(url => url.StartsWith(AllowedPrefix, StringComparison.OrdinalIgnoreCase))
            .WithMessage($"Image URL must start with '{AllowedPrefix}'");
    }
}
