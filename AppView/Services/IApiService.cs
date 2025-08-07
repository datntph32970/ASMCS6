using AppDB.Models.DtoAndViewModels.AuthService.ViewModels;
using AppDB.Models.DtoAndViewModels.BaseServices.Common;

namespace AppView.Services
{
    public interface IApiService
    {
        Task<ApiResponse<AuthResponse>> LoginAsync(LoginRequest request);
        Task<ApiResponse<AuthResponse>> RegisterAsync(RegisterRequest request);
        Task<bool> ValidateUserAsync(string username, string password);
        Task<string?> GetTokenAsync();
        Task SetTokenAsync(string token);
        Task ClearTokenAsync();
        Task<bool> IsAuthenticatedAsync();

        // User information management
        Task<UserInfo?> GetUserInfoAsync();
        Task SetUserInfoAsync(UserInfo userInfo);
        Task ClearUserInfoAsync();

        // Generic CRUD methods
        Task<ApiResponse<T>> GetAsync<T>(string endpoint);
        Task<ApiResponse<T>> PostAsync<T>(string endpoint, object data);
        Task<ApiResponse<T>> PutAsync<T>(string endpoint, object data);
        Task<ApiResponse<bool>> DeleteAsync(string endpoint);
    }
}
