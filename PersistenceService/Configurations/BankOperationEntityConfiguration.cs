using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersistenceService.Entities;

namespace PersistenceService.Configurations;

public class BankOperationEntityConfiguration : IEntityTypeConfiguration<BankOperationEntity>
{
    public void Configure(EntityTypeBuilder<BankOperationEntity> builder)
    {
        builder.HasKey(bankOperation => bankOperation.Id);
        
        builder.HasMany(bankOperation => bankOperation.SwiftMessages)
            .WithOne(swiftMessage => swiftMessage.BankOperation)
            .HasForeignKey(swiftMessage => swiftMessage.BankOperationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}