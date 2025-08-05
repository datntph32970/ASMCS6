using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDB.Models
{
    public class BaseEntity
    {
        [Key]
        public Guid id { get; set; } = Guid.NewGuid();
        public DateTime createdDate { get; set; } = DateTime.UtcNow;
        public Guid createdByName { get; set; }
        public Guid createdById { get; set; }
        public DateTime? updatedDate { get; set; }
        public Guid? updatedByName { get; set; }
        public Guid? updatedById { get; set; }
        //[ForeignKey(nameof(createdById))]
        //public virtual Users? createdBy { get; set; }
        //[ForeignKey(nameof(updatedById))]
        //public virtual Users? updatedBy { get; set; }

    }
}
