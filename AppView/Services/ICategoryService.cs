using System.Threading.Tasks;
using AppDB.Models.DtoAndViewModels.CategoriesService.ViewModels;
using AppDB.Models.DtoAndViewModels.CategoriesService.Dto;
using AppDB.Models.DtoAndViewModels.BaseServices.Common;

public interface ICategoryService
{
    Task<PagedList<CategoriesDto>> GetCategoriesAsync(CategoriesSearch search);
    Task<CategoriesDto> GetCategoryByIdAsync(Guid id);
    Task<bool> CreateCategoryAsync(CategoriesCreateVM request);
    Task<bool> UpdateCategoryAsync(CategoriesUpdateVM request);
    Task<bool> DeleteCategoryAsync(Guid id);
}