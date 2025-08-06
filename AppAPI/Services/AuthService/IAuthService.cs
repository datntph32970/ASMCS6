using AppDB.Models;
using AppDB.Models.DtoAndViewModels.AuthService.ViewModels;

namespace AppAPI.Services.AuthService
{
    public interface IAuthService
    {
        Task<ApiResponse<AuthResponse>> LoginAsync(LoginRequest request);
        Task<ApiResponse<AuthResponse>> RegisterAsync(RegisterRequest request);
        Task<bool> ValidateUserAsync(string username, string password);
    }
}