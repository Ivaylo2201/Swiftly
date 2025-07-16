using System.ComponentModel.DataAnnotations;

namespace PersistenceService.Entities;

public class AddressEntity
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public required string AddressLine { get; set; }
    
    public int UserId { get; set; }
    public UserEntity User { get; set; } = null!;
    
    public int? CityId { get; set; }
    public CityEntity? City { get; set; }
    
    public int? CountryId { get; set; }
    public CountryEntity? Country { get; set; }
}