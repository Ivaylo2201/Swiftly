using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersistenceService.Entities;

namespace PersistenceService.Configurations;

public class ChargeEntityConfiguration : IEntityTypeConfiguration<ChargeEntity>
{
    public void Configure(EntityTypeBuilder<ChargeEntity> builder)
    {
        builder.HasKey(charge => charge.Id);
        
        builder.HasMany(charge => charge.SwiftMessages)
            .WithOne(swiftMessage => swiftMessage.Charge)
            .HasForeignKey(swiftMessage => swiftMessage.ChargeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}