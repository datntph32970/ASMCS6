using AppDB.Models.DtoAndViewModels.AuthService.ViewModels;
using AppView.Models;
using Newtonsoft.Json;
using System.Text;

namespace AppView.Services
{
    public class BaseApiService : IBaseApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IJwtService _jwtService;

        public BaseApiService(HttpClient httpClient, IConfiguration configuration, IJwtService jwtService)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _jwtService = jwtService;

            // Set base address from configuration
            var apiBaseUrl = _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7294";
            _httpClient.BaseAddress = new Uri(apiBaseUrl);
        }

        private void SetAuthorizationHeader()
        {
            var token = _jwtService.GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<ApiResponse<T>> GetAsync<T>(string endpoint)
        {
            try
            {
                SetAuthorizationHeader();
                var response = await _httpClient.GetAsync(endpoint);
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ApiResponse<T>>(content);
                    return result ?? new ApiResponse<T> { Success = false, Message = "Failed to deserialize response" };
                }

                return new ApiResponse<T>
                {
                    Success = false,
                    Message = $"HTTP {response.StatusCode}: {content}"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<T>
                {
                    Success = false,
                    Message = $"Exception: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<T>> PostAsync<T>(string endpoint, object data)
        {
            try
            {
                SetAuthorizationHeader();
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(endpoint, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ApiResponse<T>>(responseContent);
                    return result ?? new ApiResponse<T> { Success = false, Message = "Failed to deserialize response" };
                }

                return new ApiResponse<T>
                {
                    Success = false,
                    Message = $"HTTP {response.StatusCode}: {responseContent}"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<T>
                {
                    Success = false,
                    Message = $"Exception: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<T>> PutAsync<T>(string endpoint, object data)
        {
            try
            {
                SetAuthorizationHeader();
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync(endpoint, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ApiResponse<T>>(responseContent);
                    return result ?? new ApiResponse<T> { Success = false, Message = "Failed to deserialize response" };
                }

                return new ApiResponse<T>
                {
                    Success = false,
                    Message = $"HTTP {response.StatusCode}: {responseContent}"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<T>
                {
                    Success = false,
                    Message = $"Exception: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse> DeleteAsync(string endpoint)
        {
            try
            {
                SetAuthorizationHeader();
                var response = await _httpClient.DeleteAsync(endpoint);
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ApiResponse>(content);
                    return result ?? new ApiResponse { Success = false, Message = "Failed to deserialize response" };
                }

                return new ApiResponse
                {
                    Success = false,
                    Message = $"HTTP {response.StatusCode}: {content}"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = $"Exception: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<T>> PostFormAsync<T>(string endpoint, IFormFile file, object data)
        {
            try
            {
                SetAuthorizationHeader();
                using var formData = new MultipartFormDataContent();

                // Add file
                if (file != null)
                {
                    var fileContent = new StreamContent(file.OpenReadStream());
                    formData.Add(fileContent, "Image", file.FileName);
                }

                // Add other data properties
                foreach (var prop in data.GetType().GetProperties())
                {
                    var value = prop.GetValue(data)?.ToString();
                    if (!string.IsNullOrEmpty(value))
                    {
                        formData.Add(new StringContent(value), prop.Name);
                    }
                }

                var response = await _httpClient.PostAsync(endpoint, formData);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ApiResponse<T>>(responseContent);
                    return result ?? new ApiResponse<T> { Success = false, Message = "Failed to deserialize response" };
                }

                return new ApiResponse<T>
                {
                    Success = false,
                    Message = $"HTTP {response.StatusCode}: {responseContent}"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<T>
                {
                    Success = false,
                    Message = $"Exception: {ex.Message}"
                };
            }
        }
    }
}