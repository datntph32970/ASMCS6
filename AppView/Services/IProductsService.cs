using AppDB.Models.DtoAndViewModels.ProductsService.Dto;
using AppDB.Models.DtoAndViewModels.ProductsService.ViewModels;
using AppView.Models;

namespace AppView.Services
{
    public interface IProductsService
    {
        Task<ApiResponse<List<ProductsDto>>> GetAllAsync();
        Task<ApiResponse<ProductsDto>> GetByIdAsync(Guid id);
        Task<ApiResponse<ProductsDto>> CreateAsync(ProductsCreateVM model);
        Task<ApiResponse<ProductsDto>> UpdateAsync(Guid id, ProductsUpdateVM model);
        Task<ApiResponse> DeleteAsync(Guid id);
        Task<ApiResponse<List<ProductsDto>>> SearchAsync(ProductsSearch searchModel);
    }
}