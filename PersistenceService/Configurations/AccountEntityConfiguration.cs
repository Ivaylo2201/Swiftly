using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersistenceService.Entities;

namespace PersistenceService.Configurations;

public class AccountEntityConfiguration : IEntityTypeConfiguration<AccountEntity>
{
    public void Configure(EntityTypeBuilder<AccountEntity> builder)
    {
        builder.HasKey(bankAccount => bankAccount.Id);
        
        builder.HasOne(bankAccount => bankAccount.User)
            .WithMany(user => user.Accounts)
            .HasForeignKey(bankAccount => bankAccount.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}