using LeekLog.Abstractions.Entites;
using LeekLog.Data.Abstractions.Stores;
using LeekLog.Services.Abstractions;

namespace LeekLog.Services;

public class ExerciseService : IExerciseService
{
    private readonly IExerciseStore _exerciseStore;

    public ExerciseService(IExerciseStore exerciseStore)
    {
        _exerciseStore = exerciseStore;
    }

    public async Task<List<ExerciseEntity>> GetAllAsync(CancellationToken ct = default)
    {
        return await _exerciseStore.GetAllAsync(ct);
    }

    public async Task SaveAsync(ExerciseEntity exercise, CancellationToken ct = default)
    {
        await _exerciseStore.SaveAsync(exercise, ct);
    }
}
