namespace LeekLog.Abstractions.Entites;

public class SessionExerciseEntity : BaseEntity
{
    public Guid SessionId { get; set; }
    public Guid ExerciseId { get; set; }

    public int Order { get; set; }

    public ExerciseEntity Exercise { get; set; } = null!;
    public List<WorkoutSetEntity> Sets { get; set; } = new();
}
