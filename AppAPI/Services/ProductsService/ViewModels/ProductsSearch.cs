using AppAPI.Services.BaseServices.Common;

namespace AppAPI.Services.ProductsService.ViewModels
{
    public class ProductsSearch : SearchBase
    {
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public Guid? CategoryID { get; set; }
    }
} 