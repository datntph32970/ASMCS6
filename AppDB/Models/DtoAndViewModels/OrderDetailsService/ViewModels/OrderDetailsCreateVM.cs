namespace AppDB.Models.DtoAndViewModels.OrderDetailsService.ViewModels
{
    public class OrderDetailsCreateVM
    {
        public Guid OrderID { get; set; }
        public Guid? ProductID { get; set; }
        public Guid? ComboID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
} 