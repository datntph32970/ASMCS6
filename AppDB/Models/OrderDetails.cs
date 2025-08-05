using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDB.Models
{
    public class OrderDetails : BaseEntity
    {
        public Guid OrderID { get; set; }
        public Guid ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        [ForeignKey(nameof(OrderID))]
        public virtual Orders Order { get; set; }
        [ForeignKey(nameof(ProductID))]
        public virtual Products Product { get; set; }
    }
}
