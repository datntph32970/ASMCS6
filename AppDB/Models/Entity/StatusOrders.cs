using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDB.Models.Entity
{
    public  class StatusOrders : BaseEntity
    {
        public Guid StatusId { get; set; }
        public Guid OrderId { get; set; }
        public virtual Status Status { get; set; } = null!;
        public virtual Orders Order { get; set; } = null!;
    }
}
