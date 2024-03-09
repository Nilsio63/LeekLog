using LeekLog.Abstractions.Entites;
using LeekLog.Abstractions.Models;
using LeekLog.Data.Abstractions.Stores;
using LeekLog.Services.Abstractions;

namespace LeekLog.Services;

public class ExerciseService : IExerciseService
{
    private readonly IExerciseStore _exerciseStore;
    private readonly IExerciseTagStore _exerciseTagStore;
    private readonly ITagStore _tagStore;

    public ExerciseService(
        IExerciseStore exerciseStore,
        IExerciseTagStore exerciseTagStore,
        ITagStore tagStore)
    {
        _exerciseStore = exerciseStore;
        _exerciseTagStore = exerciseTagStore;
        _tagStore = tagStore;
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

        string[] searchWords = GetWords(searchText);

        return searchList
            .Select(o =>
            {
                string[] titleWords = GetWords(o.Title, o.ExerciseTags.Select(o => o.Tag.Title));

                int searchHitValue = searchWords.Intersect(titleWords, StringComparer.OrdinalIgnoreCase).Count();

                return new
                {
                    FullMatch = searchText.Equals(o.Title, StringComparison.OrdinalIgnoreCase),
                    SearchHitValue = searchHitValue,
                    Exercise = o
                };
            })
            .OrderByDescending(o => o.FullMatch)
            .ThenByDescending(o => o.SearchHitValue)
            .Select(o => o.Exercise);
    }

    private static string[] GetWords(string text, IEnumerable<string>? additionalWords = null)
    {
        IEnumerable<string> wordsFromText = text.Split(new char[] { ' ', '-', '(', ')' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        if (additionalWords is not null)
        {
            wordsFromText = wordsFromText.Concat(additionalWords);
        }

        return wordsFromText
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();
    }

    public async Task SaveAsync(ExerciseEditModel exerciseEditModel, CancellationToken ct = default)
    {
        ExerciseEntity exercise = await SaveExerciseAsync(exerciseEditModel, ct);

        List<TagEntity> tags = await SaveTagsAsync(exerciseEditModel.Tags, ct);

        exercise.ExerciseTags = await SaveTagRelationsAsync(exercise, tags, ct);
    }

    private async Task<ExerciseEntity> SaveExerciseAsync(ExerciseEditModel exerciseEditModel, CancellationToken ct)
    {
        ExerciseEntity exercise = exerciseEditModel.ExerciseId.HasValue
            ? await GetByIdAsync(exerciseEditModel.ExerciseId.Value.ToString(), ct)
                ?? throw new KeyNotFoundException($"Could not find exercise with id {exerciseEditModel.ExerciseId}")
            : new();

        exercise.Title = exerciseEditModel.Title;
        exercise.Description = exerciseEditModel.Description;

        exercise.ExerciseTags = null!;

        await _exerciseStore.SaveAsync(exercise, ct);

        return exercise;
    }

    private async Task<List<TagEntity>> SaveTagsAsync(IEnumerable<string> tags, CancellationToken ct)
    {
        List<TagEntity> existingTags = await _tagStore.GetAllByTitlesAsync(tags, ct);

        string[] missingTags = tags
            .Where(tag => existingTags.Any(o => tag.Equals(o.Title, StringComparison.OrdinalIgnoreCase)) == false)
            .ToArray();

        foreach (string tag in missingTags)
        {
            TagEntity newTag = new()
            {
                Title = tag
            };

            await _tagStore.SaveAsync(newTag, ct);

            existingTags.Add(newTag);
        }

        return existingTags;
    }

    private async Task<List<ExerciseTagEntity>> SaveTagRelationsAsync(ExerciseEntity exercise, List<TagEntity> tags, CancellationToken ct)
    {
        List<ExerciseTagEntity> tagRelations = await _exerciseTagStore.GetAllByExerciseIdAsync(exercise.Id, ct);

        List<ExerciseTagEntity> toAdd = tags
            .Where(o => tagRelations.Any(r => r.TagId == o.Id) == false)
            .Select(o => new ExerciseTagEntity
            {
                TagId = o.Id,
                ExerciseId = exercise.Id
            })
            .ToList();

        List<ExerciseTagEntity> toDelete = tagRelations
            .Where(o => tags.Any(t => t.Id == o.TagId) == false)
            .ToList();

        await _exerciseTagStore.SaveAllAsync(toAdd, ct);
        await _exerciseTagStore.DeleteAllAsync(toDelete, ct);

        tagRelations = tagRelations
            .Concat(toAdd)
            .Except(toDelete)
            .ToList();

        return tagRelations;
    }
}
