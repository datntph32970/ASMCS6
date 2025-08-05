using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDB.Models
{
    public class Products : BaseEntity
    {
        public string ProductName { get; set; }
        public string? Description { get; set; }
        public string Price { get; set; }
        public string ImageURL { get; set; }
        public Guid CategoryID { get; set; }
        [ForeignKey(nameof(CategoryID))]
        public virtual Categories Category { get; set; } = null!;
        public virtual ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
        public virtual ICollection<ComboDetails> ComboDetails { get; set; } = new List<ComboDetails>();
    }
}
