using System.Globalization;
using FluentValidation;
using MessageService.DTOs;

namespace MessageService.Validators;

public class RawMessageDtoValidator : AbstractValidator<RawMessageDto>
{
    public RawMessageDtoValidator()
    {
        RuleFor(x => x.IncomingAmount)
            .NotEmpty().WithMessage("IncomingAmount is required.")
            .Must(BeValidDouble).WithMessage("IncomingAmount must be a valid number.");

        RuleFor(x => x.OutgoingAmount)
            .NotEmpty().WithMessage("OutgoingAmount is required.")
            .Must(BeValidDouble).WithMessage("OutgoingAmount must be a valid number.");

        RuleFor(x => x.ValueDate)
            .NotEmpty().WithMessage("ValueDate is required.")
            .Must(BeValidDate).WithMessage("ValueDate must be in 'yyMMdd' format.");

        RuleFor(x => x.Sender)
            .NotEmpty().WithMessage("Sender is required.")
            .Must(HaveValidAccountNumber).WithMessage("Account number must match the following regex: '^/[A-Za-z]{2}[A-Za-z0-9]*$'")
            .Must(HaveValueUsername).WithMessage("Username should not be null or empty.");
        
        RuleFor(x => x.Receiver)
            .NotEmpty().WithMessage("Receiver is required.")
            .Must(HaveValidAccountNumber).WithMessage("Account number must match the following regex: '^/[A-Za-z]{2}[A-Za-z0-9]*$'")
            .Must(HaveValueUsername).WithMessage("Username should not be null or empty.");
    }

    private static bool HaveValidAccountNumber(string value)
    {
        try
        {
            var accountNumber = value.Split("\r\n")[0];
            return Constants.Patterns.AccountNumberParseRegex().Match(accountNumber).Success;
        }
        catch (IndexOutOfRangeException)
        {
            return false;
        }
    }

    private static bool HaveValueUsername(string value)
    {
        try
        {
            var username = value.Split("\r\n")[1];
            return !string.IsNullOrWhiteSpace(username);
        }
        catch (IndexOutOfRangeException)
        {
            return false;
        }
    }

    private static bool BeValidDouble(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.EndsWith(',') || value.EndsWith('.'))
        {
            return false;
        }
        
        return double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out _);
    }

    private static bool BeValidDate(string value) =>
        DateTime.TryParseExact(value, "yyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
}