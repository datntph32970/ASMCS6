using AppAPI.Services.BaseServices;
using AppAPI.Services.BaseServices.Common;
using AppAPI.Services.UsersService.Dto;
using AppAPI.Services.UsersService.ViewModels;
using AppDB.Models;

namespace AppAPI.Services.UsersService
{
    public interface IUsersService : IBaseService<Users>
    {
        Task<PagedList<UsersDto>> GetData(UsersSearch search);
        Task<UsersDto> GetDto(Guid id);

    }
}