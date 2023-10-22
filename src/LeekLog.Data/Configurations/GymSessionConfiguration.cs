using LeekLog.Abstractions.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeekLog.Data.Configurations;

public class GymSessionConfiguration : BaseConfiguration<GymSessionEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<GymSessionEntity> builder)
    {
        builder.ToTable("GymSessions");

        builder.HasOne<UserEntity>()
            .WithMany()
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
