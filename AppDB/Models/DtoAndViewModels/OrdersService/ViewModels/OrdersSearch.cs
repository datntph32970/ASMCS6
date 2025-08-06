using AppDB.Models.DtoAndViewModels.BaseServices.Common;

namespace AppDB.Models.DtoAndViewModels.OrdersService.ViewModels
{
    public class OrdersSearch : SearchBase
    {
        public Guid? CustomerID { get; set; }
        public Guid? StaffID { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
} 