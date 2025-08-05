using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDB.Models
{
    public class Categories: BaseEntity
    {
        public string CategoryName { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<Products> Products { get; set; } = new List<Products>();
    }
}
