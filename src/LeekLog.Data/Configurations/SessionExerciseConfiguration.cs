using LeekLog.Abstractions.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeekLog.Data.Configurations;

public class SessionExerciseConfiguration : BaseConfiguration<SessionExerciseEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<SessionExerciseEntity> builder)
    {
        builder.ToTable("SessionExercises");

        builder.HasOne<GymSessionEntity>()
            .WithMany(o => o.Exercises)
            .HasForeignKey(o => o.SessionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<ExerciseEntity>()
            .WithMany()
            .HasForeignKey(o => o.ExerciseId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
