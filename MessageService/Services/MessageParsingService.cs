using LoggingService.Contracts.IServices;
using LoggingService.Enums;
using MessageService.Contracts.IServices;
using MessageService.DTOs;

namespace MessageService.Services;

public class MessageParsingService(ILoggerService loggerService) : IMessageParsingService
{
    public RawMessageDto? ParseToRawMessageDto(string rawMessage)
    {
        var match = Constants.Patterns.MessageParseRegex().Match(rawMessage);

        if (!match.Success)
        {
            loggerService.Log("[MessageParserService]: Syntax error detected, message parsing terminated.", LogType.Error);
            return null;
        }

        return new RawMessageDto
        {
            SenderBic = match.Groups["SenderBic"].Value,
            ReceiverBic = match.Groups["ReceiverBic"].Value,
            TransactionReference = match.Groups["TransactionReference"].Value,
            BankOperationCode = match.Groups["BankOperationCode"].Value,
            ValueDate = match.Groups["ValueDate"].Value,
            OutgoingCurrencyCode = match.Groups["OutgoingCurrencyCode"].Value,
            OutgoingAmount = match.Groups["OutgoingAmount"].Value,
            IncomingCurrencyCode = match.Groups["IncomingCurrencyCode"].Value,
            IncomingAmount = match.Groups["IncomingAmount"].Value,
            Sender = match.Groups["Sender"].Value,
            Issuer = match.Groups["Issuer"].Value,
            Receiver = match.Groups["Receiver"].Value,
            Reason = match.Groups["Reason"].Value,
            ChargeCode = match.Groups["Charges"].Value
        };
    }
}