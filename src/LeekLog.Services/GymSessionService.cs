using LeekLog.Abstractions.Entites;
using LeekLog.Data.Abstractions.Stores;
using LeekLog.Services.Abstractions;
using System.Text.RegularExpressions;

namespace LeekLog.Services;

public class GymSessionService : IGymSessionService
{
    private readonly IGymSessionStore _gymSessionStore;
    private readonly IExerciseService _exerciseService;

    public GymSessionService(
        IGymSessionStore gymSessionStore,
        IExerciseService exerciseService)
    {
        _gymSessionStore = gymSessionStore;
        _exerciseService = exerciseService;
    }

    public async Task<GymSessionEntity?> GetByIdAsync(string id, CancellationToken ct = default)
    {
        return await _gymSessionStore.GetByIdAsync(id, ct);
    }

    public async Task<List<GymSessionEntity>> GetAllByUserIdAsync(string userId, CancellationToken ct = default)
    {
        return await _gymSessionStore.GetAllByUserIdAsync(userId, ct);
    }

    public async Task SaveAsync(GymSessionEntity entity, CancellationToken ct = default)
    {
        await _gymSessionStore.SaveAsync(entity, ct);
    }

    public async Task DeleteAsync(Guid sessionId, CancellationToken ct = default)
    {
        await _gymSessionStore.DeleteAsync(sessionId, ct);
    }

    public async Task<GymSessionEntity> ImportAsync(string importData, CancellationToken ct = default)
    {
        GymSessionEntity newSession = new()
        {
            Date = DateOnly.FromDateTime(DateTime.Today)
        };

        string[] dataLines = importData.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        foreach (string line in dataLines)
        {
            if (TryReadDateLine(newSession, line) == false
                && TryReadDurationLine(newSession, line) == false)
            {
                await TryReadExerciseLineAsync(newSession, line, ct);
            }
        }

        return newSession;
    }

    private static bool TryReadDateLine(GymSessionEntity newSession, string line)
    {
        Regex dateRegex = new(@"Gym (\d{1,2})\.(\d{1,2})(?:\.(\d{4}))?");
        Match match = dateRegex.Match(line);

        if (match.Success)
        {
            int day = int.Parse(match.Groups[1].Value);
            int month = int.Parse(match.Groups[2].Value);
            int year = DateTime.Now.Year;

            if (match.Groups[3].Success)
            {
                year = int.Parse(match.Groups[3].Value);
            }

            newSession.Date = new DateOnly(year, month, day);

            return true;
        }

        return false;
    }

    private static bool TryReadDurationLine(GymSessionEntity newSession, string line)
    {
        Regex durationRegex = new(@"^(\d+):(\d+):(\d+)h @ (\d+) BPM$");
        Match match = durationRegex.Match(line);

        if (match.Success)
        {
            int hours = int.Parse(match.Groups[1].Value);
            int minutes = int.Parse(match.Groups[2].Value);
            int seconds = int.Parse(match.Groups[3].Value);

            newSession.Duration = new TimeSpan(hours, minutes, seconds);
            newSession.AverageHeartRate = double.Parse(match.Groups[4].Value);

            return true;
        }

        return false;
    }

    private async Task TryReadExerciseLineAsync(GymSessionEntity newSession, string line, CancellationToken ct = default)
    {
        string[] elements = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        string[] exerciseWords = elements.TakeWhile(o => o.Any(char.IsNumber) == false).ToArray();
        string exerciseName = string.Join(' ', exerciseWords);

        elements = elements.Skip(exerciseWords.Length).ToArray();

        IEnumerable<ExerciseEntity> exercises = await _exerciseService.SearchAsync(exerciseName, ct: ct);

        ExerciseEntity? matchedExercise = exercises.FirstOrDefault();

        if (matchedExercise is null)
        {
            return;
        }

        SessionExerciseEntity newExercise = new()
        {
            ExerciseId = matchedExercise.Id,
            Exercise = matchedExercise,
            Order = newSession.Exercises.Select(o => o.Order).DefaultIfEmpty().Max() + 1
        };

        Regex weightRegex = new(@"(\d+)kg");
        Regex repRegex = new(@"x(\d+)");

        WorkoutSetEntity? workoutSetEntity = null;

        double? currentWeight = null;

        while (elements.Length != 0)
        {
            string currentElement = elements[0];

            Match weightMatch = weightRegex.Match(currentElement);

            if (weightMatch.Success)
            {
                currentWeight = double.Parse(weightMatch.Groups[1].Value);
                workoutSetEntity = null;
            }
            else if (currentWeight.HasValue)
            {
                Match repsMatch = repRegex.Match(currentElement);

                if (repsMatch.Success)
                {
                    workoutSetEntity = new()
                    {
                        UsedWeight = currentWeight.Value,
                        Order = newExercise.Sets.Select(o => o.Order).DefaultIfEmpty().Max() + 1,
                        CleanRepetitions = int.Parse(repsMatch.Groups[1].Value)
                    };

                    newExercise.Sets.Add(workoutSetEntity);
                }
                else if (workoutSetEntity is not null)
                {
                    // TODO: Special reps
                }
            }

            elements = elements.Skip(1).ToArray();
        }

        newSession.Exercises.Add(newExercise);
    }
}
