using AppDB.Models.DtoAndViewModels.BaseServices.Common;

namespace AppDB.Models.DtoAndViewModels.CategoriesService.ViewModels
{
    public class CategoriesSearch : SearchBase
    {
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
    }
} 