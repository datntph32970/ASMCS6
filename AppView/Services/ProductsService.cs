using AppDB.Models.DtoAndViewModels.AuthService.ViewModels;
using AppDB.Models.DtoAndViewModels.ProductsService.Dto;
using AppDB.Models.DtoAndViewModels.ProductsService.ViewModels;
using AppView.Models;

namespace AppView.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IBaseApiService _baseApiService;

        public ProductsService(IBaseApiService baseApiService)
        {
            _baseApiService = baseApiService;
        }

        public async Task<ApiResponse<List<ProductsDto>>> GetAllAsync()
        {
            return await _baseApiService.GetAsync<List<ProductsDto>>("/api/Products");
        }

        public async Task<ApiResponse<ProductsDto>> GetByIdAsync(Guid id)
        {
            return await _baseApiService.GetAsync<ProductsDto>($"/api/Products/{id}");
        }

        public async Task<ApiResponse<ProductsDto>> CreateAsync(ProductsCreateVM model)
        {
            return await _baseApiService.PostFormAsync<ProductsDto>("/api/Products", model.Image, model);
        }

        public async Task<ApiResponse<ProductsDto>> UpdateAsync(Guid id, ProductsUpdateVM model)
        {
            return await _baseApiService.PutAsync<ProductsDto>($"/api/Products/{id}", model);
        }

        public async Task<ApiResponse> DeleteAsync(Guid id)
        {
            return await _baseApiService.DeleteAsync($"/api/Products/{id}");
        }

        public async Task<ApiResponse<List<ProductsDto>>> SearchAsync(ProductsSearch searchModel)
        {
            var queryParams = new List<string>();

            if (!string.IsNullOrEmpty(searchModel.ProductName))
                queryParams.Add($"productName={Uri.EscapeDataString(searchModel.ProductName)}");

            if (searchModel.CategoryID.HasValue)
                queryParams.Add($"categoryID={searchModel.CategoryID}");

            if (searchModel.MinPrice.HasValue)
                queryParams.Add($"minPrice={searchModel.MinPrice}");

            if (searchModel.MaxPrice.HasValue)
                queryParams.Add($"maxPrice={searchModel.MaxPrice}");

            var queryString = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";
            return await _baseApiService.GetAsync<List<ProductsDto>>($"/api/Products/search{queryString}");
        }
    }
}