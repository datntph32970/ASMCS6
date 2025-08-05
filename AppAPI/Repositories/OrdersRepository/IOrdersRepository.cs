using AppAPI.Repositories.BaseRepository;
using AppDB.Models;

namespace AppAPI.Repositories.OrdersRepository
{
    public interface IOrdersRepository : IBaseRepository<Orders>
    {
    }
} 