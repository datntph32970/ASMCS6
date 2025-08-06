using AppAPI.Services.BaseServices;
using AppDB.Models.DtoAndViewModels.BaseServices.Common;
using AppDB.Models.DtoAndViewModels.CategoriesService.Dto;
using AppDB.Models.DtoAndViewModels.CategoriesService.ViewModels;
using AppDB.Models.Entity;

namespace AppAPI.Services.CategoriesService
{
    public interface ICategoriesService : IBaseService<Categories>
    {
        Task<PagedList<CategoriesDto>> GetData(CategoriesSearch search);
        Task<CategoriesDto> GetDto(Guid id);
    }
} 