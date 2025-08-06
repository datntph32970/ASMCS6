using Microsoft.AspNetCore.Http;

namespace AppDB.Models.DtoAndViewModels.CombosService.ViewModels
{
    public class CombosCreateVM
    {
        public string ComboName { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public IFormFile Image { get; set; }
    }
} 