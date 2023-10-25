using LeekLog.Abstractions.Entites;
using LeekLog.Data.Abstractions.Stores;
using Microsoft.EntityFrameworkCore;

namespace LeekLog.Data.Stores;

public class ExerciseStore : BaseStore<ExerciseEntity>, IExerciseStore
{
    public ExerciseStore(IDbContextFactory<LeekLogDbContext> dbContextFactory)
        : base(dbContextFactory)
    {
    }
}
