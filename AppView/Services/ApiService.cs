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
                    Console.WriteLine($"Login response: Success={result?.Success}, Data={result?.Data != null}");

                    if (result?.Success == true && result.Data?.Token != null)
                    {
                        Console.WriteLine("Saving token and user info...");
                        await SetTokenAsync(result.Data.Token);

                        // Save user information
                        if (result.Data.User != null)
                        {
                            await SetUserInfoAsync(result.Data.User);
                        }

                        // Verify everything was saved
                        var savedToken = await GetTokenAsync();
                        var savedUserInfo = await GetUserInfoAsync();
                        Console.WriteLine($"Final verification - Token: {(string.IsNullOrEmpty(savedToken) ? "null" : "saved")}, User: {(savedUserInfo == null ? "null" : savedUserInfo.Username)}");
                    }
                    else
                    {
                        Console.WriteLine($"Login failed: Success={result?.Success}, Token={result?.Data?.Token != null}");
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
                    Console.WriteLine($"Register response: Success={result?.Success}, Data={result?.Data != null}");

                    if (result?.Success == true && result.Data?.Token != null)
                    {
                        Console.WriteLine("Saving token and user info from registration...");
                        await SetTokenAsync(result.Data.Token);

                        // Save user information
                        if (result.Data.User != null)
                        {
                            await SetUserInfoAsync(result.Data.User);
                        }

                        // Verify everything was saved
                        var savedToken = await GetTokenAsync();
                        var savedUserInfo = await GetUserInfoAsync();
                        Console.WriteLine($"Final verification - Token: {(string.IsNullOrEmpty(savedToken) ? "null" : "saved")}, User: {(savedUserInfo == null ? "null" : savedUserInfo.Username)}");
                    }
                    else
                    {
                        Console.WriteLine($"Registration failed: Success={result?.Success}, Token={result?.Data?.Token != null}");
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
                var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", TokenKey);
                Console.WriteLine($"Token retrieved: {(string.IsNullOrEmpty(token) ? "null" : token.Substring(0, Math.Min(20, token.Length)) + "...")}");
                return token;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting token: {ex.Message}");
                return null;
            }
        }

        public async Task SetTokenAsync(string token)
        {
            try
            {
                Console.WriteLine($"Setting token: {token.Substring(0, Math.Min(20, token.Length))}...");
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TokenKey, token);

                // Verify token was saved
                var savedToken = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", TokenKey);
                Console.WriteLine($"Token verification: {(string.IsNullOrEmpty(savedToken) ? "failed" : "success")}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving token: {ex.Message}");
            }
        }

        public async Task ClearTokenAsync()
        {
            try
            {
                Console.WriteLine("Clearing token...");
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TokenKey);
                Console.WriteLine("Token cleared");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error clearing token: {ex.Message}");
            }
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            var token = await GetTokenAsync();
            var isAuthenticated = !string.IsNullOrEmpty(token);
            Console.WriteLine($"IsAuthenticatedAsync: {isAuthenticated}");
            return isAuthenticated;
        }

        // User information management methods
        public async Task<UserInfo?> GetUserInfoAsync()
        {
            try
            {
                var userInfoJson = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", UserInfoKey);
                Console.WriteLine($"User info retrieved: {(string.IsNullOrEmpty(userInfoJson) ? "null" : userInfoJson)}");
                if (string.IsNullOrEmpty(userInfoJson))
                    return null;

                return JsonSerializer.Deserialize<UserInfo>(userInfoJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting user info: {ex.Message}");
                return null;
            }
        }

        public async Task SetUserInfoAsync(UserInfo userInfo)
        {
            try
            {
                Console.WriteLine($"Setting user info: {userInfo.Username}");
                var userInfoJson = JsonSerializer.Serialize(userInfo);
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", UserInfoKey, userInfoJson);

                // Verify user info was saved
                var savedUserInfo = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", UserInfoKey);
                Console.WriteLine($"User info verification: {(string.IsNullOrEmpty(savedUserInfo) ? "failed" : "success")}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving user info: {ex.Message}");
            }
        }

        public async Task ClearUserInfoAsync()
        {
            try
            {
                Console.WriteLine("Clearing user info...");
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", UserInfoKey);
                Console.WriteLine("User info cleared");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error clearing user info: {ex.Message}");
            }
        }

        // Generic CRUD methods
        public async Task<ApiResponse<T>> GetAsync<T>(string endpoint)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}/{endpoint}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ApiResponse<T>>();
                    return result ?? new ApiResponse<T> { Success = false, Message = "Failed to deserialize response" };
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return new ApiResponse<T> { Success = false, Message = $"Request failed: {response.StatusCode}" };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<T> { Success = false, Message = $"Request error: {ex.Message}" };
            }
        }

        public async Task<ApiResponse<T>> PostAsync<T>(string endpoint, object data)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/{endpoint}", data);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ApiResponse<T>>();
                    return result ?? new ApiResponse<T> { Success = false, Message = "Failed to deserialize response" };
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return new ApiResponse<T> { Success = false, Message = $"Request failed: {response.StatusCode}" };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<T> { Success = false, Message = $"Request error: {ex.Message}" };
            }
        }

        public async Task<ApiResponse<T>> PutAsync<T>(string endpoint, object data)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{endpoint}", data);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ApiResponse<T>>();
                    return result ?? new ApiResponse<T> { Success = false, Message = "Failed to deserialize response" };
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return new ApiResponse<T> { Success = false, Message = $"Request failed: {response.StatusCode}" };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<T> { Success = false, Message = $"Request error: {ex.Message}" };
            }
        }

        public async Task<ApiResponse<bool>> DeleteAsync(string endpoint)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{BaseUrl}/{endpoint}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ApiResponse<bool>>();
                    return result ?? new ApiResponse<bool> { Success = false, Message = "Failed to deserialize response" };
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return new ApiResponse<bool> { Success = false, Message = $"Request failed: {response.StatusCode}" };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool> { Success = false, Message = $"Request error: {ex.Message}" };
            }
        }


    }
}
