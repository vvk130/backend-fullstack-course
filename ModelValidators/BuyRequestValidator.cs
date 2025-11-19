using FluentValidation;
using System;

public class BuyRequestValidator : AbstractValidator<BuyRequest>
{
    public BuyRequestValidator()
    {
        RuleFor(x => x.BuyerId)
            .NotEmpty()
            .WithMessage("BuyerId cannot be empty.");

        RuleFor(x => x.AdId)
            .NotEmpty()
            .WithMessage("AdId cannot be empty.");

        RuleFor(x => x.ItemType)
            .IsInEnum()
            .WithMessage("ItemType must be either Horse or Alpaca.");

        RuleFor(x => x.Bid)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Bid must be zero or greater.");
    }
}
