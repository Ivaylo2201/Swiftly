using MessageService.DTOs;

namespace MessageService.Contracts.IServices;

public interface IMessageParsingService
{
    Task<RawMessageDto?> ParseToRawMessageDto(string rawMessage);
}