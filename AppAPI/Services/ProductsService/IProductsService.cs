using AppAPI.Services.BaseServices;
using AppDB.Models.DtoAndViewModels.BaseServices.Common;
using AppDB.Models.DtoAndViewModels.ProductsService.Dto;
using AppDB.Models.DtoAndViewModels.ProductsService.ViewModels;
using AppDB.Models.Entity;

namespace AppAPI.Services.ProductsService
{
    public interface IProductsService : IBaseService<Products>
    {
        Task<PagedList<ProductsDto>> GetData(ProductsSearch search);
        Task<ProductsDto> GetDto(Guid id);
    }
} 