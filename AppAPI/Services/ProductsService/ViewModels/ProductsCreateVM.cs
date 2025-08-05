namespace AppAPI.Services.ProductsService.ViewModels
{
    public class ProductsCreateVM
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string ImageURL { get; set; }
        public Guid CategoryID { get; set; }
    }
} 