using System.Threading.Tasks;
using AppDB.Models.DtoAndViewModels.AuthService.ViewModels;

public interface IAuthService
{
    Task<ApiResponse<AuthResponse>> LoginAsync(LoginRequest request);
    Task<ApiResponse<AuthResponse>> RegisterAsync(RegisterRequest request);
}