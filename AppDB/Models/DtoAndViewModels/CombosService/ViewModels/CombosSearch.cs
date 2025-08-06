using AppDB.Models.DtoAndViewModels.BaseServices.Common;

namespace AppDB.Models.DtoAndViewModels.CombosService.ViewModels
{
    public class CombosSearch : SearchBase
    {
        public string? ComboName { get; set; }
        public string? Description { get; set; }
    }
} 