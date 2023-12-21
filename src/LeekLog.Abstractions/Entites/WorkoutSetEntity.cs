namespace LeekLog.Abstractions.Entites;

public class WorkoutSetEntity : BaseEntity
{
    public Guid SessionExerciseId { get; set; }

    public int Order { get; set; }
    public int CleanRepetitions { get; set; }
    public int AssistedRepetitions { get; set; }
    public int UncleanRepetitions { get; set; }
    public int FailedRepetitions { get; set; }
    public int PartialRepetitions { get; set; }
    public int NegativeRepetitions { get; set; }
    public double UsedWeight { get; set; }
}
