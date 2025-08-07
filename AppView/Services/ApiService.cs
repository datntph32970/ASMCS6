using AppDB.Models.DtoAndViewModels.AuthService.ViewModels;
using AppDB.Models.DtoAndViewModels.BaseServices.Common;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Text.Json;

namespace AppView.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private const string TokenKey = "auth_token";
        private const string UserInfoKey = "user_info";
        private const string BaseUrl = "https://localhost:7294/api";

        public ApiService(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }

        public async Task<ApiResponse<AuthResponse>> LoginAsync(LoginRequest request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/Auth/login", request);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ApiResponse<AuthResponse>>();

                    if (result?.Success == true && result.Data?.Token != null)
                    {
                        await SetTokenAsync(result.Data.Token);

                        // Save user information
                        if (result.Data.User != null)
                        {
                            await SetUserInfoAsync(result.Data.User);
                        }
                    }

                    return result ?? new ApiResponse<AuthResponse> { Success = false, Message = "Login failed" };
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return new ApiResponse<AuthResponse> { Success = false, Message = $"Login failed: {response.StatusCode}" };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<AuthResponse> { Success = false, Message = $"Login error: {ex.Message}" };
            }
        }

        public async Task<ApiResponse<AuthResponse>> RegisterAsync(RegisterRequest request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/Auth/register", request);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ApiResponse<AuthResponse>>();

                    if (result?.Success == true && result.Data?.Token != null)
                    {
                        await SetTokenAsync(result.Data.Token);

                        // Save user information
                        if (result.Data.User != null)
                        {
                            await SetUserInfoAsync(result.Data.User);
                        }
                    }

                    return result ?? new ApiResponse<AuthResponse> { Success = false, Message = "Registration failed" };
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return new ApiResponse<AuthResponse> { Success = false, Message = $"Registration failed: {response.StatusCode}" };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<AuthResponse> { Success = false, Message = $"Registration error: {ex.Message}" };
            }
        }

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}/Auth/validate?username={username}&password={password}");
                return await response.Content.ReadFromJsonAsync<bool>();
            }
            catch
            {
                return false;
            }
        }

        public async Task<string?> GetTokenAsync()
        {
            try
            {
                return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", TokenKey);
            }
            catch
            {
                return null;
            }
        }

        public async Task SetTokenAsync(string token)
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TokenKey, token);
            }
            catch
            {
                // Handle error silently
            }
        }

        public async Task ClearTokenAsync()
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TokenKey);
            }
            catch
            {
                // Handle error silently
            }
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            var token = await GetTokenAsync();
            return !string.IsNullOrEmpty(token);
        }

        // User information management methods
        public async Task<UserInfo?> GetUserInfoAsync()
        {
            try
            {
                var userInfoJson = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", UserInfoKey);
                if (string.IsNullOrEmpty(userInfoJson))
                    return null;

                return JsonSerializer.Deserialize<UserInfo>(userInfoJson);
            }
            catch
            {
                return null;
            }
        }

        public async Task SetUserInfoAsync(UserInfo userInfo)
        {
            try
            {
                var userInfoJson = JsonSerializer.Serialize(userInfo);
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", UserInfoKey, userInfoJson);
            }
            catch
            {
                // Handle error silently
            }
        }

        public async Task ClearUserInfoAsync()
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", UserInfoKey);
            }
            catch
            {
                // Handle error silently
            }
        }
    }
}
