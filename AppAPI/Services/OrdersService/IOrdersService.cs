using AppAPI.Services.BaseServices;
using AppAPI.Services.BaseServices.Common;
using AppAPI.Services.OrdersService.Dto;
using AppAPI.Services.OrdersService.ViewModels;
using AppDB.Models.Entity;

namespace AppAPI.Services.OrdersService
{
    public interface IOrdersService : IBaseService<Orders>
    {
        Task<PagedList<OrdersDto>> GetData(OrdersSearch search);
        Task<OrdersDto> GetDto(Guid id);
    }
} 