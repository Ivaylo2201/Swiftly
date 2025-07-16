using System.ComponentModel.DataAnnotations;

namespace PersistenceService.Entities;

public class CurrencyEntity
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(3)]
    public required string CurrencyCode { get; set; }
    
    public ICollection<SwiftMessageEntity> IncomingSwiftMessages { get; set; } = [];
    public ICollection<SwiftMessageEntity> OutgoingSwiftMessages { get; set; } = [];
}