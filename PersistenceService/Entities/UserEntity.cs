using System.ComponentModel.DataAnnotations;

namespace PersistenceService.Entities;

public class UserEntity
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(25)]
    public required string UserName { get; set; }

    public ICollection<AccountEntity> Accounts { get; set; } = [];
    public ICollection<AddressEntity> Addresses { get; set; } = [];
    public ICollection<SwiftMessageEntity> SentMessages { get; set; } = [];
    public ICollection<SwiftMessageEntity> ReceivedMessages { get; set; } = [];
}