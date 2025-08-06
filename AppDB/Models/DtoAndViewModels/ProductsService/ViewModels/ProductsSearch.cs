using AppDB.Models.DtoAndViewModels.BaseServices.Common;

namespace AppDB.Models.DtoAndViewModels.ProductsService.ViewModels
{
    public class ProductsSearch : SearchBase
    {
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public Guid? CategoryID { get; set; }
    }
} 