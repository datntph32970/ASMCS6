using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using AppDB.Models.DtoAndViewModels.AuthService.ViewModels;
using Microsoft.Extensions.Configuration;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public AuthService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _baseUrl = configuration["ApiSettings:BaseUrl"];
    }

    public async Task<ApiResponse<AuthResponse>> LoginAsync(LoginRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(_baseUrl + "api/Auth/login", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ApiResponse<AuthResponse>>();
    }

    public async Task<ApiResponse<AuthResponse>> RegisterAsync(RegisterRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(_baseUrl + "api/Auth/register", request);
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            return JsonSerializer.Deserialize<ApiResponse<AuthResponse>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        else
        {
            // Trả về lỗi chi tiết từ API
            var error = JsonSerializer.Deserialize<ApiResponse<AuthResponse>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return error ?? new ApiResponse<AuthResponse> { Success = false, Message = "Đăng ký thất bại!" };
        }
    }
}