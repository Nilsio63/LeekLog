namespace LeekLog.Abstractions.Models;

public class ExerciseEditModel
{
    public Guid? ExerciseId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();
}
