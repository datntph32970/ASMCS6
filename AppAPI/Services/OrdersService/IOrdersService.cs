using AppAPI.Services.BaseServices;
using AppDB.Models.DtoAndViewModels.BaseServices.Common;
using AppDB.Models.DtoAndViewModels.OrdersService.Dto;
using AppDB.Models.DtoAndViewModels.OrdersService.ViewModels;
using AppDB.Models.Entity;

namespace AppAPI.Services.OrdersService
{
    public interface IOrdersService : IBaseService<Orders>
    {
        Task<PagedList<OrdersDto>> GetData(OrdersSearch search);
        Task<OrdersDto> GetDto(Guid id);
    }
} 