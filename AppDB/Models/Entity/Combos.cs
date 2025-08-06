using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDB.Models.Entity
{
    public class Combos: BaseEntity
    {
        public string ComboName { get; set; }
        public string? Description { get; set; }
        public string Price { get; set; }
        public string ImageURL { get; set; }
        public virtual ICollection<ComboDetails> ComboDetails { get; set; } = new List<ComboDetails>();
        public virtual ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
    }
}
