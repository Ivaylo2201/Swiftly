using System.ComponentModel.DataAnnotations;

namespace PersistenceService.Entities;

public class SwiftFileEntity
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public required string SwiftFileName { get; set; }
    
    [Required]
    public required int SwiftMessageCount { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public required bool IsSuccess { get; init; }
}