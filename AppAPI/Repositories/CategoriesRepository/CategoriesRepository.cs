using AppAPI.Repositories.BaseRepository;
using AppAPI.Repositories.ComboDetailsRepository;
using AppDB;
using AppDB.Models;

namespace AppAPI.Repositories.CategoriesRepository
{
    public class CategoriesRepository : BaseRepository<Categories>, ICategoriesRepository
    {
        public CategoriesRepository(AppDBContext entities) : base(entities)
        {
        }
    }
} 