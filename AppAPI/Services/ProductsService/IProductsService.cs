using AppAPI.Services.BaseServices;
using AppAPI.Services.BaseServices.Common;
using AppAPI.Services.ProductsService.Dto;
using AppAPI.Services.ProductsService.ViewModels;
using AppDB.Models.Entity;

namespace AppAPI.Services.ProductsService
{
    public interface IProductsService : IBaseService<Products>
    {
        Task<PagedList<ProductsDto>> GetData(ProductsSearch search);
        Task<ProductsDto> GetDto(Guid id);
    }
} 