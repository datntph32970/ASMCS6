using AppAPI.Services.BaseServices;
using AppAPI.Services.BaseServices.Common;
using AppAPI.Services.RolesService.Dto;
using AppAPI.Services.RolesService.ViewModels;
using AppDB.Models;

namespace AppAPI.Services.RolesService
{
    public interface IRolesService : IBaseService<Roles>
    {
        Task<PagedList<RolesDto>> GetData(RolesSearch search);
        Task<RolesDto> GetDto(Guid id);
    }
}
