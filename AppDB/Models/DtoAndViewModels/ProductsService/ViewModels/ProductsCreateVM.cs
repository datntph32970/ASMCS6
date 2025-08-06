using Microsoft.AspNetCore.Http;

namespace AppDB.Models.DtoAndViewModels.ProductsService.ViewModels
{
    public class ProductsCreateVM
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IFormFile Image { get; set; }
        public Guid CategoryID { get; set; }
    }
} 