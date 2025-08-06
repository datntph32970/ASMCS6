using AppDB.Models;

namespace AppDB.Models.DtoAndViewModels.OrdersService.ViewModels
{
    public class  OrdersCreateVM
    {
        public Guid CustomerID { get; set; }
        public decimal TotalAmount { get; set; }
    }
}