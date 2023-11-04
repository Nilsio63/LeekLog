namespace LeekLog.Abstractions.Entites;

public class GymSessionEntity : BaseEntity
{
    public Guid UserId { get; set; }

    public DateOnly Date { get; set; }
    public TimeSpan? Duration { get; set; }
    public double? AverageHeartRate { get; set; }

    public List<SessionExerciseEntity> Exercises { get; set; } = new();
}
