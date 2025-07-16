using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersistenceService.Entities;

namespace PersistenceService.Configurations;

public class AddressEntityConfiguration : IEntityTypeConfiguration<AddressEntity>
{
    public void Configure(EntityTypeBuilder<AddressEntity> builder)
    {
        builder.HasKey(address => address.Id);
        
        builder.HasOne(address => address.User)
            .WithMany(user => user.Addresses)
            .HasForeignKey(address => address.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(address => address.Country)
            .WithMany(country => country.Addresses)
            .HasForeignKey(address => address.CountryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(address => address.City)
            .WithMany(city => city.Addresses)
            .HasForeignKey(address => address.CityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}