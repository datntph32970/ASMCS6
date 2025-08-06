using AppAPI.Repositories.BaseRepository;
using AppDB;
using AppDB.Models.Entity;

namespace AppAPI.Repositories.OrdersRepository
{
    public class OrdersRepository : BaseRepository<Orders>, IOrdersRepository
    {
        public OrdersRepository(AppDBContext entities) : base(entities)
        {
        }
    }
} 