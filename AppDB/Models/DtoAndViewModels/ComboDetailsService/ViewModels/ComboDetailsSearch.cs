using AppDB.Models.DtoAndViewModels.BaseServices.Common;

namespace AppDB.Models.DtoAndViewModels.ComboDetailsService.ViewModels
{
    public class ComboDetailsSearch : SearchBase
    {
        public Guid? ComboID { get; set; }
        public Guid? ProductID { get; set; }
    }
} 