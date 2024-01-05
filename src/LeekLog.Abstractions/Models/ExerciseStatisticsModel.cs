namespace LeekLog.Abstractions.Models;

public class ExerciseStatisticsModel
{
    public DateOnly Date { get; set; }
    public int SetCount { get; set; }
    public double MaxWeight { get; set; }
    public int MaxRepetitions { get; set; }
    public double TotalVolume { get; set; }
}
