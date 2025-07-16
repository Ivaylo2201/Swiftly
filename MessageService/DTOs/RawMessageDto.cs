namespace MessageService.DTOs;

public record RawMessageDto
{
    public required string SenderBic { get; init; }
    public required string ReceiverBic { get; init; }
    public required string TransactionReference { get; init; }
    public required string BankOperationCode { get; init; }
    public required string ValueDate { get; init; }
    public required string OutgoingCurrencyCode { get; init; }
    public required string OutgoingAmount { get; init; }
    public required string IncomingCurrencyCode { get; init; }
    public required string IncomingAmount { get; init; }
    public required string Sender { get; init; }
    public required string Issuer { get; init; }
    public required string Receiver { get; init; }
    public required string Reason { get; init; }
    public required string ChargeCode { get; init; }
}