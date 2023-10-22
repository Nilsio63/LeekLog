﻿namespace LeekLog.Abstractions.Entites;

public class WorkoutSetEntity : BaseEntity
{
    public Guid SessionExerciseId { get; set; }

    public int Order { get; set; }
    public int NumberOfRepetitions { get; set; }
    public double UsedWeight { get; set; }
}