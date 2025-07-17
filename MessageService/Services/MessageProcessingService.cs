using System.Globalization;
using MessageService.Abstractions;
using MessageService.Contracts.IServices;
using MessageService.Validators;
using PersistenceService.Contracts.IRepositories;
using PersistenceService.Entities;
using PersistenceService.Exceptions;
using Shared.Enums;
using Shared.Producer;
using Shared.Requests;

namespace MessageService.Services;

public class MessageProcessingService(
    IBankOperationEntityRepository bankOperationRepository,
    ICurrencyEntityRepository currencyRepository,
    IUserEntityRepository userRepository,
    IChargeEntityRepository chargeRepository,
    ISwiftMessageEntityRepository swiftMessageRepository,
    IMessageParsingService messageParsingService,
    IProducer producer) : IMessageProcessingService
{
    private string ServiceName => GetType().Name;
    
    public async Task<Result<SwiftMessageEntity>> Process(string rawMessage, int index)
    {
        var rawMessageDto = await messageParsingService.ParseToRawMessageDto(rawMessage);
        
        if (rawMessageDto is null)
            return Result.Failure<SwiftMessageEntity>();
        
        var result = await new RawMessageDtoValidator().ValidateAsync(rawMessageDto);
        
        if (!result.IsValid)
        {
            var errors = string.Join(", ", result.Errors.Select(error => error.ErrorMessage));

            await producer.PublishToLoggingService(new LoggingRequest
            {
                Message = $"[{ServiceName}]: Validation errors at Message #{index} - {errors}",
                LogType = LogType.Error
            });
            
            return Result.Failure<SwiftMessageEntity>(errors);
        }

        try
        {
            var senderData = rawMessageDto.Sender.Split("\r\n");
            var receiverData = rawMessageDto.Receiver.Split("\r\n");
            
            var bankOperation = await bankOperationRepository.GetByBankOperationCodeAsync(rawMessageDto.BankOperationCode);
            var incomingCurrency = await currencyRepository.GetByCurrencyCodeAsync(rawMessageDto.IncomingCurrencyCode);
            var outgoingCurrency = await currencyRepository.GetByCurrencyCodeAsync(rawMessageDto.OutgoingCurrencyCode);
            var charge = await chargeRepository.GetByChargeCodeAsync(rawMessageDto.ChargeCode);
            
            var sender = await userRepository.GetOrCreateAsync(
                new UserEntity { UserName = senderData[1] },
                new AccountEntity { Number = senderData[0], Bic = rawMessageDto.SenderBic },
                new AddressEntity { AddressLine = senderData[2] });
            
            var receiver = await userRepository.GetOrCreateAsync(
                new UserEntity { UserName = receiverData[1] },
                new AccountEntity { Number = receiverData[0], Bic = rawMessageDto.ReceiverBic },
                new AddressEntity { AddressLine = receiverData[2] });

            var swiftMessage = new SwiftMessageEntity
            {
                TransactionReference = rawMessageDto.TransactionReference.Replace("\r\n", ""),
                IncomingAmount = double.Parse(rawMessageDto.IncomingAmount.Replace(',', '.'), CultureInfo.InvariantCulture),
                OutgoingAmount = double.Parse(rawMessageDto.OutgoingAmount.Replace(',', '.'), CultureInfo.InvariantCulture),
                CreatedAt = DateTime.ParseExact(rawMessageDto.ValueDate, "yyMMdd", CultureInfo.InvariantCulture),
                BankOperationId = bankOperation.Id,
                IncomingCurrencyId = incomingCurrency.Id,
                OutgoingCurrencyId = outgoingCurrency.Id,
                SenderId = sender.Id,
                ReceiverId = receiver.Id,
                Issuer = rawMessageDto.Issuer.Replace("\r\n", "").Trim(),
                Reason = rawMessageDto.Reason.Replace("\r\n", "").Replace(@"\RFB\", "").Trim(),
                ChargeId = charge.Id
            };
            
            await swiftMessageRepository.CreateAsync(swiftMessage);
            
            await producer.PublishToLoggingService(new LoggingRequest
            {
                Message = $"[{ServiceName}]: Message #{index} successfully processed to a SwiftMessage",
                LogType = LogType.Success
            });
            
            return Result.Failure<SwiftMessageEntity>("Syntax error");
        }
        catch (EntityNotFoundException ex)
        {
            await producer.PublishToLoggingService(new LoggingRequest
            {
                Message = $"[{ServiceName}]: {ex.GetType().FullName} at Message #{index} - {ex.Message}",
                LogType = LogType.Error
            });
            
            return Result.Failure<SwiftMessageEntity>(ex.Message);
        }
        catch (Exception ex)
        {
            await producer.PublishToLoggingService(new LoggingRequest
            {
                Message = $"[{ServiceName}]: {ex.GetType().FullName} at Message #{index} - {ex.Message}",
                LogType = LogType.Error
            });
            
            return Result.Failure<SwiftMessageEntity>(ex.Message);
        }
    }
}