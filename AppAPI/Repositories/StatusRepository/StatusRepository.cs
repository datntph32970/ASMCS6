using AppAPI.Repositories.BaseRepository;
using AppDB;
using AppDB.Models.Entity;

namespace AppAPI.Repositories.StatusRepository
{
    public class StatusRepository : BaseRepository<Status>, IStatusRepository
    {
        public StatusRepository(AppDBContext context) : base(context)
        {
        }
    }
}