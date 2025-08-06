using AppAPI.Services.BaseServices;
using AppDB.Models.DtoAndViewModels.BaseServices.Common;
using AppDB.Models.DtoAndViewModels.StatusOrdersService.Dto;
using AppDB.Models.DtoAndViewModels.StatusOrdersService.ViewModels;
using AppDB.Models.Entity;

namespace AppAPI.Services.StatusOrdersService
{
    public interface IStatusOrdersService : IBaseService<StatusOrders>
    {
        Task<PagedList<StatusOrdersDto>> GetData(StatusOrdersSearch search);
        Task<StatusOrdersDto?> GetDto(Guid id);
    }
}