using System.ComponentModel.DataAnnotations;

namespace PersistenceService.Entities;

public class ChargeEntity
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(3)]
    public required string ChargeCode { get; set; }
    
    public ICollection<SwiftMessageEntity> SwiftMessages { get; set; } = [];
}