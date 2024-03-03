namespace LeekLog.Abstractions.Entites;

public class ExerciseTagEntity : BaseEntity
{
    public Guid ExerciseId { get; set; }
    public Guid TagId { get; set; }

    public ExerciseEntity Exercise { get; set; } = null!;
    public TagEntity Tag { get; set; } = null!;
}
