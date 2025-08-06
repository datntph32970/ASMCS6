using AppAPI.Repositories.BaseRepository;
using AppDB.Models.Entity;

namespace AppAPI.Repositories.ProductsRepository
{
    public interface IProductsRepository : IBaseRepository<Products>
    {
    }
} 