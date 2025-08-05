using AppAPI.Services.BaseServices.Common;

namespace AppAPI.Services.CombosService.ViewModels
{
    public class CombosSearch : SearchBase
    {
        public string ComboName { get; set; }
        public string Description { get; set; }
    }
} 