using WatchReadShare.Application.Contracts.Persistence;

namespace WatchReadShare.Persistence
{
    public class UnitOfWork(Context context) : IUnitOfWork
    {
        public Task<int> SaveChangesAsync() => context.SaveChangesAsync();

    }
}
