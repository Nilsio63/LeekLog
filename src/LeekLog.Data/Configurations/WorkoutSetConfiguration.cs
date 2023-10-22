using LeekLog.Abstractions.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeekLog.Data.Configurations;

public class WorkoutSetConfiguration : BaseConfiguration<WorkoutSetEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<WorkoutSetEntity> builder)
    {
        builder.ToTable("WorkoutSetEntity");

        builder.HasOne<SessionExerciseEntity>()
            .WithMany(o => o.Sets)
            .HasForeignKey(o => o.SessionExerciseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
