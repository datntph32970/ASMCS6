using AppDB.Models.DtoAndViewModels.BaseServices.Common;

namespace AppDB.Models.DtoAndViewModels.OrderDetailsService.ViewModels
{
    public class OrderDetailsSearch : SearchBase
    {
        public Guid? OrderID { get; set; }
        public Guid? ProductID { get; set; }
        public Guid? ComboID { get; set; }
    }
} 