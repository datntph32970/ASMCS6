using AppDB.Models.DtoAndViewModels.AuthService.ViewModels;

namespace AppView.Services
{
    public interface IUserService
    {
        Task<UserInfo?> GetCurrentUserAsync();
        Task<string?> GetCurrentUserNameAsync();
        Task<string?> GetCurrentUserRoleAsync();
        Task<bool> IsCurrentUserInRoleAsync(string role);
        Task ClearCurrentUserAsync();
    }
}
