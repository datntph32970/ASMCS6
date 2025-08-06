using AppDB.Models.DtoAndViewModels.AuthService.ViewModels;
using AppDB.Models.DtoAndViewModels.CategoriesService.Dto;
using AppDB.Models.DtoAndViewModels.CategoriesService.ViewModels;
using AppView.Models;

namespace AppView.Services
{
    public interface ICategoriesService
    {
        Task<ApiResponse<List<CategoriesDto>>> GetAllAsync();
        Task<ApiResponse<CategoriesDto>> GetByIdAsync(Guid id);
        Task<ApiResponse<CategoriesDto>> CreateAsync(CategoriesCreateVM model);
        Task<ApiResponse<CategoriesDto>> UpdateAsync(Guid id, CategoriesUpdateVM model);
        Task<ApiResponse> DeleteAsync(Guid id);
        Task<ApiResponse<List<CategoriesDto>>> SearchAsync(CategoriesSearch searchModel);
    }
}