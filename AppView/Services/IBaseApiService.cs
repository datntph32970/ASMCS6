using AppView.Models;

namespace AppView.Services
{
    public interface IBaseApiService
    {
        Task<ApiResponse<T>> GetAsync<T>(string endpoint);
        Task<ApiResponse<T>> PostAsync<T>(string endpoint, object data);
        Task<ApiResponse<T>> PutAsync<T>(string endpoint, object data);
        Task<ApiResponse> DeleteAsync(string endpoint);
        Task<ApiResponse<T>> PostFormAsync<T>(string endpoint, IFormFile file, object data);
    }
}