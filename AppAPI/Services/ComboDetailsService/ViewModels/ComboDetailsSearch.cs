using AppAPI.Services.BaseServices.Common;

namespace AppAPI.Services.ComboDetailsService.ViewModels
{
    public class ComboDetailsSearch : SearchBase
    {
        public Guid? ComboID { get; set; }
        public Guid? ProductID { get; set; }
    }
} 