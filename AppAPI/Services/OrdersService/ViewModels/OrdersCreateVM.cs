using AppDB.Models;

namespace AppAPI.Services.OrdersService.ViewModels
{
    public class  OrdersCreateVM
    {
        public Guid CustomerID { get; set; }
        public decimal TotalAmount { get; set; }
    }
}