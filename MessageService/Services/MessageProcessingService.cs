using MessageService.Abstractions;
using MessageService.Contracts.IServices;
using MessageService.Validators;
using PersistenceService.Entities;

namespace MessageService.Services;

public class MessageProcessingService(IMessageParsingService messageParsingService) : IMessageProcessingService
{
    public async Task<Result<SwiftMessageEntity>> Process(string rawMessage)
    {
        var rawMessageDto = messageParsingService.ParseToRawMessageDto(rawMessage);

        if (rawMessageDto == null)
            return Result.Failure<SwiftMessageEntity>("Syntax error");
        
        var result = await new RawMessageDtoValidator().ValidateAsync(rawMessageDto);
        
        if (!result.IsValid)
        {
            var errors = string.Join(", ", result.Errors.Select(error => error.ErrorMessage));
            
            // Call rabbitMq
            // loggerService.Log($"[MessageProcessingService]: Validation errors - {errors}", LogType.Error);
            
            return Result.Failure<SwiftMessageEntity>(errors);
        }

        try
        {
            var senderData = rawMessageDto.Sender.Split("\r\n");
            var receiverData = rawMessageDto.Receiver.Split("\r\n");
            
            // TODO: Replace with RabbitMQ call
            // This eliminates the need to import the repos (intoroduce dependencies)
            // The repos must listen for calls from other services
            
            // var bankOperation = await bankOperationRepository.GetByBankOperationCodeAsync(rawMessageDto.BankOperationCode);
            // var incomingCurrency = await currencyRepository.GetByCurrencyCodeAsync(rawMessageDto.IncomingCurrencyCode);
            // var outgoingCurrency = await currencyRepository.GetByCurrencyCodeAsync(rawMessageDto.OutgoingCurrencyCode);
            // var charge = await chargeRepository.GetByChargeCodeAsync(rawMessageDto.ChargeCode);
            //
            // var sender = await userRepository.GetOrCreateAsync(
            //     new UserEntity { UserName = senderData[1] },
            //     new AccountEntity { Number = senderData[0], Bic = rawMessageDto.SenderBic },
            //     new AddressEntity { AddressLine = senderData[2] });
            //
            // var receiver = await userRepository.GetOrCreateAsync(
            //     new UserEntity { UserName = receiverData[1] },
            //     new AccountEntity { Number = receiverData[0], Bic = rawMessageDto.ReceiverBic },
            //     new AddressEntity { AddressLine = receiverData[2] });

            // var swiftMessage = new SwiftMessageEntity
            // {
            //     TransactionReference = rawMessageDto.TransactionReference.Replace("\r\n", ""),
            //     IncomingAmount = double.Parse(rawMessageDto.IncomingAmount.Replace(',', '.'), CultureInfo.InvariantCulture),
            //     OutgoingAmount = double.Parse(rawMessageDto.OutgoingAmount.Replace(',', '.'), CultureInfo.InvariantCulture),
            //     CreatedAt = DateTime.ParseExact(rawMessageDto.ValueDate, "yyMMdd", CultureInfo.InvariantCulture),
            //     BankOperationId = bankOperation.Id,
            //     IncomingCurrencyId = incomingCurrency.Id,
            //     OutgoingCurrencyId = outgoingCurrency.Id,
            //     SenderId = sender.Id,
            //     ReceiverId = receiver.Id,
            //     Issuer = rawMessageDto.Issuer.Replace("\r\n", "").Trim(),
            //     Reason = rawMessageDto.Reason.Replace("\r\n", "").Replace(@"\RFB\", "").Trim(),
            //     ChargeId = charge.Id
            // };
            
            // await swiftMessageRepository.CreateAsync(swiftMessage);
            //
            // Call rabbitMq
            // loggerService.Log("[MessageProcessingService]: RawMessageDto successfully processed to a SwiftMessage", LogType.Success);
            
            return Result.Failure<SwiftMessageEntity>("Syntax error");
            // return Result.Success(swiftMessage);
        }
        // catch (EntityNotFoundException exception)
        // {
        //     // Call rabbitMq
        //     // loggerService.Log($"[MessageProcessingService]: EntityNotFoundException - {exception.Message}", LogType.Error);
        //     return Result.Failure<SwiftMessageDto>(exception.Message);
        // }
        catch (Exception exception)
        {
            // Call rabbitMq
            // loggerService.Log($"[MessageProcessingService]: Exception - {exception.Message}", LogType.Error);
            return Result.Failure<SwiftMessageEntity>(exception.Message);
        }
    }
}