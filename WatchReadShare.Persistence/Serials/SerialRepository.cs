using WatchReadShare.Application.Contracts.Persistence;
using WatchReadShare.Domain.Entities;

namespace WatchReadShare.Persistence.Serials
{
    public class SerialRepository(Context context) : GenericRepository<Serial, int>(context), ISerialRepository
    {
    }
}
