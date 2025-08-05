using AppAPI.Services.AuthService.ViewModels;
using AppDB.Models;

namespace AppAPI.Services.AuthService
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<bool> ValidateUserAsync(string username, string password);
    }
}