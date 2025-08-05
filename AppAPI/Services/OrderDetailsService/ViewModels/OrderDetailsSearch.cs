using AppAPI.Services.BaseServices.Common;

namespace AppAPI.Services.OrderDetailsService.ViewModels
{
    public class OrderDetailsSearch : SearchBase
    {
        public Guid? OrderID { get; set; }
        public Guid? ProductID { get; set; }
    }
} 