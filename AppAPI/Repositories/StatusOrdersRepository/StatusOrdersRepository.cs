using AppAPI.Repositories.BaseRepository;
using AppDB;
using AppDB.Models.Entity;

namespace AppAPI.Repositories.StatusOrdersRepository
{
    public class StatusOrdersRepository : BaseRepository<StatusOrders>, IStatusOrdersRepository
    {
        public StatusOrdersRepository(AppDBContext context) : base(context)
        {
        }
    }
}