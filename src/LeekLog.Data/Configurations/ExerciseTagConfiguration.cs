using LeekLog.Abstractions.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeekLog.Data.Configurations;

public class ExerciseTagConfiguration : BaseConfiguration<ExerciseTagEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<ExerciseTagEntity> builder)
    {
        builder.ToTable("ExerciseTags");

        builder.HasOne(o => o.Tag)
            .WithMany()
            .HasForeignKey(o => o.TagId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(o => o.Exercise)
            .WithMany(o => o.ExerciseTags)
            .HasForeignKey(o => o.ExerciseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(o => new { o.ExerciseId, o.TagId }).IsUnique();
    }
}
