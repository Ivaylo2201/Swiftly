using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersistenceService.Entities;

namespace PersistenceService.Configurations;

public class SwiftFileEntityConfiguration : IEntityTypeConfiguration<SwiftFileEntity>
{
    public void Configure(EntityTypeBuilder<SwiftFileEntity> builder)
    {
        builder.HasKey(swiftFile => swiftFile.Id);
    }
}