using AppAPI.Repositories.BaseRepository;
using AppAPI.Services.BaseServices;
using AppAPI.Services.BaseServices.Common;
using AppAPI.Services.UsersService.Dto;
using AppAPI.Services.UsersService.ViewModels;
using AppDB.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace AppAPI.Services.UsersService
{
    public class UsersService : BaseService<Users>, IUsersService
    {
        public UsersService(IBaseRepository<Users> repository) : base(repository)
        {
        }

        public async Task<PagedList<UsersDto>> GetData(UsersSearch search)
        {
            var query = from u in GetQueryable()
                        select new UsersDto
                        {
                            id = u.id,
                            Username = u.Username,
                            Password = u.Password,
                            Phone = u.Phone,
                            Email = u.Email,
                            Address = u.Address,
                            FullName = u.FullName,
                            RoleId = u.RoleId,
                            Role = u.Role,
                            createdById = u.createdById,
                            createdByName = u.createdByName,
                            createdDate = u.createdDate,
                            updatedById = u.updatedById,
                            updatedByName = u.updatedByName,
                            updatedDate = u.updatedDate
                        };
            if (search != null)
            {
                if (!string.IsNullOrEmpty(search.Username))
                    query = query.Where(x => x.Username.Contains(search.Username));
                if (!string.IsNullOrEmpty(search.FullName))
                    query = query.Where(x => x.FullName.Contains(search.FullName));
                if (!string.IsNullOrEmpty(search.Email))
                    query = query.Where(x => x.Email.Contains(search.Email));
                if (!string.IsNullOrEmpty(search.Phone))
                    query = query.Where(x => x.Phone.Contains(search.Phone));
            }
            query = query.OrderByDescending(x => x.createdDate);
            return await PagedList<UsersDto>.CreateAsync(query, search);
        }

        public async Task<UsersDto> GetDto(Guid id)
        {
            var item = await (from u in GetQueryable().Where(x => x.id == id)
                              select new UsersDto
                              {
                                  id = u.id,
                                  Username = u.Username,
                                  Password = u.Password,
                                  Phone = u.Phone,
                                  Email = u.Email,
                                  Address = u.Address,
                                  FullName = u.FullName,
                                  RoleId = u.RoleId,
                                  Role = u.Role,
                                  createdById = u.createdById,
                                  createdByName = u.createdByName,
                                  createdDate = u.createdDate,
                                  updatedById = u.updatedById,
                                  updatedByName = u.updatedByName,
                                  updatedDate = u.updatedDate
                              }).FirstOrDefaultAsync();

            return item;
        }



        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}