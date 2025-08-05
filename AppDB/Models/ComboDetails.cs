using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDB.Models
{
    public class ComboDetails : BaseEntity
    {
        public Guid ComboID { get; set; }
        public Guid ProductID { get; set; }
        public int Quantity { get; set; }
        [ForeignKey(nameof(ComboID))]
        public virtual Combos Combo { get; set; }
        [ForeignKey(nameof(ProductID))]
        public virtual Products Product { get; set; }
    }
}
