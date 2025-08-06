using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AppDB.Models.DtoAndViewModels.CategoriesService.ViewModels;
using AppDB.Models.DtoAndViewModels.CategoriesService.Dto;
using AppDB.Models.DtoAndViewModels.BaseServices.Common;
using AppDB.Models.DtoAndViewModels.AuthService.ViewModels;
using Microsoft.Extensions.Configuration;

public class CategoryService : ICategoryService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public CategoryService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _baseUrl = configuration["ApiSettings:BaseUrl"];
    }

    public async Task<PagedList<CategoriesDto>> GetCategoriesAsync(CategoriesSearch search)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}api/Categories?CategoryName={search.CategoryName}&Description={search.Description}&PageIndex={search.PageIndex}&PageSize={search.PageSize}");
        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            throw new UnauthorizedAccessException("Phiên đăng nhập đã hết hạn, vui lòng đăng nhập lại!");
        var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<PagedList<CategoriesDto>>>();
        return apiResponse?.Data;
    }

    public async Task<CategoriesDto> GetCategoryByIdAsync(Guid id)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}api/Categories/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            throw new UnauthorizedAccessException("Phiên đăng nhập đã hết hạn, vui lòng đăng nhập lại!");
        var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<CategoriesDto>>();
        return apiResponse?.Data;
    }

    public async Task<bool> CreateCategoryAsync(CategoriesCreateVM request)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}api/Categories", request);
        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            throw new UnauthorizedAccessException("Phiên đăng nhập đã hết hạn, vui lòng đăng nhập lại!");
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateCategoryAsync(CategoriesUpdateVM request)
    {
        var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}api/Categories", request);
        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            throw new UnauthorizedAccessException("Phiên đăng nhập đã hết hạn, vui lòng đăng nhập lại!");
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteCategoryAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"{_baseUrl}api/Categories/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            throw new UnauthorizedAccessException("Phiên đăng nhập đã hết hạn, vui lòng đăng nhập lại!");
        return response.IsSuccessStatusCode;
    }
}