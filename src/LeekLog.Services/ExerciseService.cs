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

    public async Task<ExerciseEntity?> GetByIdAsync(string id, CancellationToken ct = default)
    {
        return await _exerciseStore.GetByIdAsync(id, ct);
    }

    public async Task<List<ExerciseEntity>> GetAllAsync(CancellationToken ct = default)
    {
        return await _exerciseStore.GetAllAsync(ct);
    }

    public async Task<IEnumerable<ExerciseEntity>> SearchAsync(string searchText, IEnumerable<ExerciseEntity>? searchList = null, CancellationToken ct = default)
    {
        searchList ??= await GetAllAsync(ct);

        if (string.IsNullOrEmpty(searchText))
        {
            return searchList.ToList();
        }

        return searchList.Where(o => o.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase));
    }

    public async Task SaveAsync(ExerciseEntity exercise, CancellationToken ct = default)
    {
        await _exerciseStore.SaveAsync(exercise, ct);
    }
}
