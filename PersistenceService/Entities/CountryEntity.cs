using System.ComponentModel.DataAnnotations;

namespace PersistenceService.Entities;

public class CountryEntity
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(25)]
    public required string CountryName { get; set; }
    
    public ICollection<AddressEntity> Addresses { get; set; } = [];
}