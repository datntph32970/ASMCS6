using AppDB.Models;

namespace AppAPI.Services.OrdersService.ViewModels
{
    public class OrdersUpdateVM : OrdersCreateVM
    {
        public Guid Id { get; set; }
    }
} 