using AppAPI.Services.BaseServices;
using AppDB.Models.DtoAndViewModels.BaseServices.Common;
using AppDB.Models.DtoAndViewModels.StatusService.Dto;
using AppDB.Models.DtoAndViewModels.StatusService.ViewModels;
using AppDB.Models.Entity;

namespace AppAPI.Services.StatusService
{
    public interface IStatusService : IBaseService<Status>
    {
        Task<PagedList<StatusDto>> GetData(StatusSearch search);
        Task<StatusDto?> GetDto(Guid id);
    }
}