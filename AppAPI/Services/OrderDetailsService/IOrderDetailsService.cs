using AppAPI.Services.BaseServices;
using AppAPI.Services.BaseServices.Common;
using AppAPI.Services.OrderDetailsService.Dto;
using AppAPI.Services.OrderDetailsService.ViewModels;
using AppDB.Models.Entity;

namespace AppAPI.Services.OrderDetailsService
{
    public interface IOrderDetailsService : IBaseService<OrderDetails>
    {
        Task<PagedList<OrderDetailsDto>> GetData(OrderDetailsSearch search);
        Task<OrderDetailsDto> GetDto(Guid id);
    }
} 