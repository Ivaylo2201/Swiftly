using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PersistenceService.Entities;

public class SwiftMessageEntity
{
    public int Id { get; set; }

    [Required]
    [MaxLength(35)]
    public required string TransactionReference { get; set; } = null!;

    [Required]
    [MaxLength(255)]
    public required string Reason { get; set; }

    [Required]
    [Precision(18, 2)]
    public required double IncomingAmount { get; set; }
    
    [Required]
    [Precision(18, 2)]
    public required double OutgoingAmount { get; set; }

    [Required]
    public required DateTime CreatedAt { get; set; }
    
    public int BankOperationId { get; set; }
    public BankOperationEntity BankOperation { get; set; } = null!;
    
    public int IncomingCurrencyId { get; set; }
    public CurrencyEntity IncomingCurrency { get; set; } = null!;
    
    public int OutgoingCurrencyId { get; set; }
    public CurrencyEntity OutgoingCurrency { get; set; } = null!;
    
    public int SenderId { get; set; }
    public UserEntity Sender { get; set; } = null!;
    
    public int ReceiverId { get; set; }
    public UserEntity Receiver { get; set; } = null!;
    
    [Required]
    [MaxLength(20)]
    public required string Issuer { get; set; }
    
    public int ChargeId { get; set; }
    public ChargeEntity Charge { get; set; } = null!;
}