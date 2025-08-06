using AppAPI.Repositories.BaseRepository;
using AppDB;
using AppDB.Models.Entity;

namespace AppAPI.Repositories.ComboDetailsRepository
{
    public class ComboDetailsRepository : BaseRepository<ComboDetails>, IComboDetailsRepository
    {
        public ComboDetailsRepository(AppDBContext entities) : base(entities)
        {
        }
    }
} 