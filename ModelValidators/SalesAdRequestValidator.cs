using GameModel;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using FluentValidation;

public class SalesAdRequestValidator : AbstractValidator<SalesAdRequest>
{
    public SalesAdRequestValidator(){

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than zero.");

        RuleFor(x => x.daysAdIsValid)
            .InclusiveBetween(1, 60)
            .WithMessage("Days valid must be between 1 and 60.");

        RuleFor(x => x.ItemType)
            .IsInEnum()
            .WithMessage("ItemType must be either Horse or Alpaca.");

        RuleFor(x => x.HorseId)
            .NotEmpty()
            .WithMessage("HorseId cannot be empty.");

        RuleFor(x => x.OwnerId)
            .NotEmpty()
            .WithMessage("OwnerId cannot be empty.");
    }
}

