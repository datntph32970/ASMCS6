
using AppDB.Models.DtoAndViewModels.AuthService.ViewModels;

namespace AppView.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseApiService _baseApiService;

        public AuthService(IBaseApiService baseApiService)
        {
            _baseApiService = baseApiService;
        }

        public async Task<ApiResponse<AuthResponse>> LoginAsync(LoginRequest model)
        {
            return await _baseApiService.PostAsync<AuthResponse>("/api/Auth/login", model);
        }

        public async Task<ApiResponse<AuthResponse>> RegisterAsync(RegisterRequest model)
        {
            return await _baseApiService.PostAsync<AuthResponse>("/api/Auth/register", model);
        }

        public async Task<ApiResponse> LogoutAsync()
        {
            // Logout chỉ cần clear token ở client, không cần gọi API
            return new ApiResponse { Success = true, Message = "Logout successful" };
        }
    }
}