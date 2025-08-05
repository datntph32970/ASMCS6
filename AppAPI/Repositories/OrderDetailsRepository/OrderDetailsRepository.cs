using AppAPI.Repositories.BaseRepository;
using AppDB;
using AppDB.Models;

namespace AppAPI.Repositories.OrderDetailsRepository
{
    public class OrderDetailsRepository : BaseRepository<OrderDetails>, IOrderDetailsRepository
    {
        public OrderDetailsRepository(AppDBContext entities) : base(entities)
        {
        }
    }
} 