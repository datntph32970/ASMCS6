using AppDB.Models.DtoAndViewModels.AuthService.ViewModels;
using AppDB.Models.DtoAndViewModels.RolesService.Dto;
using AppView.Models;

namespace AppView.Services
{
    public interface IRolesService
    {
        Task<ApiResponse<List<RolesDto>>> GetAllAsync();
        Task<ApiResponse<RolesDto>> GetByIdAsync(Guid id);
    }
}