using AppDB.Models.DtoAndViewModels.CategoriesService.Dto;
using AppDB.Models.DtoAndViewModels.CategoriesService.ViewModels;
using AppView.Models;

namespace AppView.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IBaseApiService _baseApiService;

        public CategoriesService(IBaseApiService baseApiService)
        {
            _baseApiService = baseApiService;
        }

        public async Task<ApiResponse<List<CategoriesDto>>> GetAllAsync()
        {
            return await _baseApiService.GetAsync<List<CategoriesDto>>("/api/Categories");
        }

        public async Task<ApiResponse<CategoriesDto>> GetByIdAsync(Guid id)
        {
            return await _baseApiService.GetAsync<CategoriesDto>($"/api/Categories/{id}");
        }

        public async Task<ApiResponse<CategoriesDto>> CreateAsync(CategoriesCreateVM model)
        {
            return await _baseApiService.PostAsync<CategoriesDto>("/api/Categories", model);
        }

        public async Task<ApiResponse<CategoriesDto>> UpdateAsync(Guid id, CategoriesUpdateVM model)
        {
            return await _baseApiService.PutAsync<CategoriesDto>($"/api/Categories/{id}", model);
        }

        public async Task<ApiResponse> DeleteAsync(Guid id)
        {
            return await _baseApiService.DeleteAsync($"/api/Categories/{id}");
        }

        public async Task<ApiResponse<List<CategoriesDto>>> SearchAsync(CategoriesSearch searchModel)
        {
            var queryParams = new List<string>();

            if (!string.IsNullOrEmpty(searchModel.CategoryName))
                queryParams.Add($"categoryName={Uri.EscapeDataString(searchModel.CategoryName)}");

            if (!string.IsNullOrEmpty(searchModel.Description))
                queryParams.Add($"description={Uri.EscapeDataString(searchModel.Description)}");

            var queryString = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";
            return await _baseApiService.GetAsync<List<CategoriesDto>>($"/api/Categories/search{queryString}");
        }
    }
}