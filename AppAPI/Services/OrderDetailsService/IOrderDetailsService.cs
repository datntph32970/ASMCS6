using AppAPI.Services.BaseServices;
using AppDB.Models.DtoAndViewModels.BaseServices.Common;
using AppDB.Models.DtoAndViewModels.OrderDetailsService.Dto;
using AppDB.Models.DtoAndViewModels.OrderDetailsService.ViewModels;
using AppDB.Models.Entity;

namespace AppAPI.Services.OrderDetailsService
{
    public interface IOrderDetailsService : IBaseService<OrderDetails>
    {
        Task<PagedList<OrderDetailsDto>> GetData(OrderDetailsSearch search);
        Task<OrderDetailsDto> GetDto(Guid id);
    }
} 