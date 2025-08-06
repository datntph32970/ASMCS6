using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDB.Models.Entity
{
    public class Roles: BaseEntity
    {
        public string RoleName { get; set; }
        public virtual ICollection<Users> Users { get; set; } = new List<Users>();
    }
}
