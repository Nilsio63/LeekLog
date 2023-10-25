using LeekLog.Abstractions.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeekLog.Data.Configurations;

public class ExerciseConfiguration : BaseConfiguration<ExerciseEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<ExerciseEntity> builder)
    {
        builder.ToTable("Exercises");

        builder.HasIndex(o => o.Title).IsUnique();
    }
}
