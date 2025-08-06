using AppDB.Models.DtoAndViewModels.AuthService.ViewModels;
using AppView.Models;

namespace AppView.Services
{
    public interface IAuthService
    {
        Task<ApiResponse<AuthResponse>> LoginAsync(LoginRequest model);
        Task<ApiResponse<AuthResponse>> RegisterAsync(RegisterRequest model);
        Task<ApiResponse> LogoutAsync();
    }
}