using AppAPI.Services.BaseServices;
using AppAPI.Services.BaseServices.Common;
using AppAPI.Services.StatusService.Dto;
using AppAPI.Services.StatusService.ViewModels;
using AppDB.Models;

namespace AppAPI.Services.StatusService
{
    public interface IStatusService : IBaseService<Status>
    {
        Task<PagedList<StatusDto>> GetData(StatusSearch search);
        Task<StatusDto?> GetDto(Guid id);
    }
}