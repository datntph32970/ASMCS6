using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDB.Models
{
    public class Orders : BaseEntity
    {
        public Guid CustomerID { get; set; }
        public Guid? StaffID { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }
        [ForeignKey(nameof(CustomerID))]
        public virtual Users Customer { get; set; } = null!;
        [ForeignKey(nameof(StaffID))]
        public virtual Users Staff { get; set; } = null!;
        public virtual ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
        public virtual ICollection<StatusOrders> StatusOrders { get; set; } = new List<StatusOrders>();
    }
  
}
