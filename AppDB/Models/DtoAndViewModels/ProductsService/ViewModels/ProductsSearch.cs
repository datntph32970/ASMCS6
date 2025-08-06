using AppDB.Models.DtoAndViewModels.BaseServices.Common;

namespace AppDB.Models.DtoAndViewModels.ProductsService.ViewModels
{
    public class ProductsSearch : SearchBase
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public Guid? CategoryID { get; set; }
    }
} 