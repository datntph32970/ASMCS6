using AppAPI.Repositories.BaseRepository;
using AppDB;
using AppDB.Models.Entity;

namespace AppAPI.Repositories.CombosRepository
{
    public class CombosRepository : BaseRepository<Combos>, ICombosRepository
    {
        public CombosRepository(AppDBContext entities) : base(entities)
        {
        }
    }
} 