using MessageService.DTOs;

namespace MessageService.Contracts.IServices;

public interface IMessageParsingService
{
    RawMessageDto? ParseToRawMessageDto(string rawMessage);
}