using AppAPI.Services.BaseServices;
using AppAPI.Services.BaseServices.Common;
using AppAPI.Services.StatusOrdersService.Dto;
using AppAPI.Services.StatusOrdersService.ViewModels;
using AppDB.Models.Entity;

namespace AppAPI.Services.StatusOrdersService
{
    public interface IStatusOrdersService : IBaseService<StatusOrders>
    {
        Task<PagedList<StatusOrdersDto>> GetData(StatusOrdersSearch search);
        Task<StatusOrdersDto?> GetDto(Guid id);
    }
}