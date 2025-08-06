using AppAPI.Services.BaseServices;
using AppDB.Models.DtoAndViewModels.BaseServices.Common;
using AppDB.Models.DtoAndViewModels.RolesService.Dto;
using AppDB.Models.DtoAndViewModels.RolesService.ViewModels;
using AppDB.Models.Entity;

namespace AppAPI.Services.RolesService
{
    public interface IRolesService : IBaseService<Roles>
    {
        Task<PagedList<RolesDto>> GetData(RolesSearch search);
        Task<RolesDto> GetDto(Guid id);
    }
}
