using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersistenceService.Entities;

namespace PersistenceService.Configurations;

public class CityEntityConfiguration : IEntityTypeConfiguration<CityEntity>
{
    public void Configure(EntityTypeBuilder<CityEntity> builder)
    {
        builder.HasKey(city => city.Id);
        
        builder.HasMany(city => city.Addresses)
            .WithOne(address => address.City)
            .HasForeignKey(address => address.CityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}