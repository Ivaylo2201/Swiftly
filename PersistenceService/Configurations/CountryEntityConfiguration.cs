using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersistenceService.Entities;

namespace PersistenceService.Configurations;

public class CountryEntityConfiguration : IEntityTypeConfiguration<CountryEntity>
{
    public void Configure(EntityTypeBuilder<CountryEntity> builder)
    {
        builder.HasKey(country => country.Id);
        
        builder.HasMany(country => country.Addresses)
            .WithOne(address => address.Country)
            .HasForeignKey(address => address.CityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}