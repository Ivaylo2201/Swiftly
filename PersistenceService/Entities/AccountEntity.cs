using System.ComponentModel.DataAnnotations;

namespace PersistenceService.Entities;

public class AccountEntity
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(75)]
    public required string Number { get; set; }
    
    [Required]
    [MaxLength(75)]
    public required string Bic { get; set; }
    
    public int UserId { get; set; }
    public UserEntity User { get; set; } = null!;
}