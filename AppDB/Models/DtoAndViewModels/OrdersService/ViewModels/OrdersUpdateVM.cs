using AppDB.Models;

namespace AppDB.Models.DtoAndViewModels.OrdersService.ViewModels
{
    public class OrdersUpdateVM : OrdersCreateVM
    {
        public Guid Id { get; set; }
    }
} 