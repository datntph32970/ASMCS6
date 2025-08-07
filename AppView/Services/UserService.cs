using AppDB.Models.DtoAndViewModels.AuthService.ViewModels;

namespace AppView.Services
{
    public class UserService : IUserService
    {
        private readonly IApiService _apiService;

        public UserService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<UserInfo?> GetCurrentUserAsync()
        {
            return await _apiService.GetUserInfoAsync();
        }

        public async Task<string?> GetCurrentUserNameAsync()
        {
            var userInfo = await GetCurrentUserAsync();
            return userInfo?.FullName ?? userInfo?.Username;
        }

        public async Task<string?> GetCurrentUserRoleAsync()
        {
            var userInfo = await GetCurrentUserAsync();
            return userInfo?.RoleName;
        }

        public async Task<bool> IsCurrentUserInRoleAsync(string role)
        {
            var currentRole = await GetCurrentUserRoleAsync();
            return !string.IsNullOrEmpty(currentRole) && currentRole.Equals(role, StringComparison.OrdinalIgnoreCase);
        }

        public async Task ClearCurrentUserAsync()
        {
            await _apiService.ClearUserInfoAsync();
        }
    }
}
