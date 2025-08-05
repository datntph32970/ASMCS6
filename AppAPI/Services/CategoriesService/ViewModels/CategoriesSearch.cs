using AppAPI.Services.BaseServices.Common;

namespace AppAPI.Services.CategoriesService.ViewModels
{
    public class CategoriesSearch : SearchBase
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
} 