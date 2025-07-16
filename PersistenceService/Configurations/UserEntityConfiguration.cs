using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersistenceService.Entities;

namespace PersistenceService.Configurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(user => user.Id);
        
        builder.HasMany(user => user.Accounts)
            .WithOne(account => account.User)
            .HasForeignKey(bankAccount => bankAccount.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(user => user.Addresses)
            .WithOne(address => address.User)
            .HasForeignKey(address => address.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(user => user.SentMessages)
            .WithOne(swiftMessage => swiftMessage.Sender)
            .HasForeignKey(swiftMessage => swiftMessage.SenderId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(user => user.ReceivedMessages)
            .WithOne(swiftMessage => swiftMessage.Receiver)
            .HasForeignKey(swiftMessage => swiftMessage.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}