using AppAPI.Repositories.BaseRepository;
using AppDB;
using AppDB.Models;

namespace AppAPI.Repositories.ProductsRepository
{
    public class ProductsRepository : BaseRepository<Products>, IProductsRepository
    {
        public ProductsRepository(AppDBContext entities) : base(entities)
        {
        }
    }
} 