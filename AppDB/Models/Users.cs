using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDB.Models
{
    public class Users : BaseEntity
    {
        [Required]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Độ dài username phải từ 10 - 50 kí tự")]
        public string Username { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Độ dài password phải từ 6 - 50 kí tự")]
        public string Password { get; set; }
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Số điện thoại không hợp lệ")]
        [DisplayName("Số điện thoại")]
        public string Phone { get; set; }
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }
        [StringLength(500, ErrorMessage = "Địa chỉ không được vượt quá 500 kí tự")]
        public string Address { get; set; }
        public string FullName { get; set; }
        public Guid RoleId { get; set; }
        [ForeignKey(nameof(RoleId))]
        public virtual Roles Role { get; set; }
        public virtual ICollection<Orders> Customer_Orders { get; set; } = new List<Orders>();
        public virtual ICollection<Orders> Staff_Orders { get; set; } = new List<Orders>();
    }
}
