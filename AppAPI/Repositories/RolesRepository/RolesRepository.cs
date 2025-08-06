using AppAPI.Repositories.BaseRepository;
using AppDB;
using AppDB.Models.Entity;

namespace AppAPI.Repositories.RolesRepository
{
    public class RolesRepository : BaseRepository<Roles>, IRolesRepository
    {
        public RolesRepository(AppDBContext entities) : base(entities)
        {
        }
    }
}
