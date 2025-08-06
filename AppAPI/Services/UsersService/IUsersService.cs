using AppAPI.Services.BaseServices;
using AppDB.Models.DtoAndViewModels.BaseServices.Common;
using AppDB.Models.DtoAndViewModels.UsersService.Dto;
using AppDB.Models.DtoAndViewModels.UsersService.ViewModels;
using AppDB.Models.Entity;

namespace AppAPI.Services.UsersService
{
    public interface IUsersService : IBaseService<Users>
    {
        Task<PagedList<UsersDto>> GetData(UsersSearch search);
        Task<UsersDto> GetDto(Guid id);

    }
}