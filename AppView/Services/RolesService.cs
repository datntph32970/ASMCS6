using AppDB.Models.DtoAndViewModels.RolesService.Dto;
using AppView.Models;

namespace AppView.Services
{
    public class RolesService : IRolesService
    {
        private readonly IBaseApiService _baseApiService;

        public RolesService(IBaseApiService baseApiService)
        {
            _baseApiService = baseApiService;
        }

        public async Task<ApiResponse<List<RolesDto>>> GetAllAsync()
        {
            return await _baseApiService.GetAsync<List<RolesDto>>("/api/Roles");
        }

        public async Task<ApiResponse<RolesDto>> GetByIdAsync(Guid id)
        {
            return await _baseApiService.GetAsync<RolesDto>($"/api/Roles/{id}");
        }
    }
}