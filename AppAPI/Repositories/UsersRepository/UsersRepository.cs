using AppAPI.Repositories.BaseRepository;
using AppDB;
using AppDB.Models.Entity;

namespace AppAPI.Repositories.UsersRepository
{
    public class UsersRepository : BaseRepository<Users>, IUsersRepository
    {
        public UsersRepository(AppDBContext entities) : base(entities)
        {
        }
    }
} 