using LeekLog.Abstractions.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeekLog.Data.Configurations;

public class TagConfiguration : BaseConfiguration<TagEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<TagEntity> builder)
    {
        builder.ToTable("Tags");

        builder.HasIndex(o => o.Title).IsUnique();
    }
}
