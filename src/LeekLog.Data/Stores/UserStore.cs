using LeekLog.Abstractions.Entites;
using Microsoft.EntityFrameworkCore;

namespace LeekLog.Data.Stores;

public interface IUserStore : IBaseStore<UserEntity>
{
}

public class UserStore : BaseStore<UserEntity>, IUserStore
{
    public UserStore(IDbContextFactory<LeekLogDbContext> dbContextFactory)
        : base(dbContextFactory)
    {
    }
}
