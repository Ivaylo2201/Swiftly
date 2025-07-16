using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersistenceService.Entities;

namespace PersistenceService.Configurations;

public class CurrencyEntityConfiguration : IEntityTypeConfiguration<CurrencyEntity>
{
    public void Configure(EntityTypeBuilder<CurrencyEntity> builder)
    {
        builder.HasKey(currency => currency.Id);
        
        builder.HasMany(currency => currency.IncomingSwiftMessages)
            .WithOne(swiftMessage => swiftMessage.IncomingCurrency)
            .HasForeignKey(swiftMessage => swiftMessage.IncomingCurrencyId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(currency => currency.OutgoingSwiftMessages)
            .WithOne(swiftMessage => swiftMessage.OutgoingCurrency)
            .HasForeignKey(swiftMessage => swiftMessage.OutgoingCurrencyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}