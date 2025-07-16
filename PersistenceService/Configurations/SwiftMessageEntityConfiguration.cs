using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersistenceService.Entities;

namespace PersistenceService.Configurations;

public class SwiftMessageEntityConfiguration : IEntityTypeConfiguration<SwiftMessageEntity>
{
    public void Configure(EntityTypeBuilder<SwiftMessageEntity> builder)
    {
        builder.HasKey(swiftMessage => swiftMessage.Id);
        
        builder.HasOne(swiftMessage => swiftMessage.BankOperation)
            .WithMany(bankOperation => bankOperation.SwiftMessages)
            .HasForeignKey(swiftMessage => swiftMessage.BankOperationId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(swiftMessage => swiftMessage.IncomingCurrency)
            .WithMany(currency => currency.IncomingSwiftMessages)
            .HasForeignKey(swiftMessage => swiftMessage.IncomingCurrencyId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(swiftMessage => swiftMessage.OutgoingCurrency)
            .WithMany(currency => currency.OutgoingSwiftMessages)
            .HasForeignKey(swiftMessage => swiftMessage.OutgoingCurrencyId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(swiftMessage => swiftMessage.Sender)
            .WithMany(user => user.SentMessages)
            .HasForeignKey(swiftMessage => swiftMessage.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(swiftMessage => swiftMessage.Receiver)
            .WithMany(user => user.ReceivedMessages)
            .HasForeignKey(swiftMessage => swiftMessage.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}