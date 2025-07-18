using MessageService.Contracts.IServices;
using MessageService.DTOs;
using Shared;
using Shared.Enums;
using Shared.Producer;
using Shared.Requests;

namespace MessageService.Services;

public class MessageParsingService(IProducer producer) : IMessageParsingService
{
    private string ServiceName => GetType().Name;
    
    public async Task<RawMessageDto?> ParseToRawMessageDto(string rawMessage)
    {
        var match = Constants.Patterns.MessageParseRegex().Match(rawMessage);

        if (!match.Success)
        {
            await producer.PublishAsync(
                Queues.Logging,
                new LoggingRequest
                {
                    Message = $"[{ServiceName}]: Syntax error detected, message parsing terminated.",
                    LogType = LogType.Error
                });

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