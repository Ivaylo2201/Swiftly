using System.ComponentModel.DataAnnotations;

namespace PersistenceService.Entities;

public class CityEntity
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(25)]
    public required string CityName { get; set; }
    
    public ICollection<AddressEntity> Addresses { get; set; } = [];
}