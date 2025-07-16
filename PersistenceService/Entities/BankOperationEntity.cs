using System.ComponentModel.DataAnnotations;

namespace PersistenceService.Entities;

public class BankOperationEntity
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(4)]
    public required string BankOperationCode { get; set; }

    public ICollection<SwiftMessageEntity> SwiftMessages { get; set; } = [];
}