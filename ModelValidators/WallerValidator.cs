using FluentValidation;
using System;

public record WalletCreateDto(int Balance, Guid OwnerId);

public class WalletCreateDtoValidator : AbstractValidator<WalletCreateDto>
{
    public WalletCreateDtoValidator()
    {
        RuleFor(x => x.Balance)
            .InclusiveBetween(0, 100000)
            .WithMessage("Balance must be between 0 and 100,000.");

        RuleFor(x => x.OwnerId)
            .NotEmpty()
            .WithMessage("Owner Id cannot be empty.");
    }
}
