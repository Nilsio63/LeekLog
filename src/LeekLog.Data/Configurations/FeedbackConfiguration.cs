using LeekLog.Abstractions.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeekLog.Data.Configurations;

public class FeedbackConfiguration : BaseConfiguration<FeedbackEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<FeedbackEntity> builder)
    {
        builder.ToTable("Feedbacks");

        builder.HasOne<UserEntity>()
            .WithMany()
            .HasForeignKey(o => o.UserId);

        builder.HasIndex(o => new { o.UserId, o.Date }).IsUnique();
        builder.HasIndex(o => o.UserId);
    }
}
