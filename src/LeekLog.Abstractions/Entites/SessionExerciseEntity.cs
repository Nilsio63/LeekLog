namespace LeekLog.Abstractions.Entites;

public class SessionExerciseEntity : BaseEntity
{
    public Guid SessionId { get; set; }
    public Guid ExerciseId { get; set; }

    public int Order { get; set; }

    public List<WorkoutSetEntity> Sets { get; set; } = new();
}
