using AppAPI.Services.BaseServices;
using AppAPI.Services.BaseServices.Common;
using AppAPI.Services.CategoriesService.Dto;
using AppAPI.Services.CategoriesService.ViewModels;
using AppDB.Models;

namespace AppAPI.Services.CategoriesService
{
    public interface ICategoriesService : IBaseService<Categories>
    {
        Task<PagedList<CategoriesDto>> GetData(CategoriesSearch search);
        Task<CategoriesDto> GetDto(Guid id);
    }
} 