using System.ComponentModel.DataAnnotations;

namespace AppAPI.Services.StatusOrdersService.ViewModels
{
    public class StatusOrdersCreateVM
    {
        [Required(ErrorMessage = "ID đơn hàng là bắt buộc")]
        public Guid OrderId { get; set; }

        [Required(ErrorMessage = "ID trạng thái là bắt buộc")]
        public Guid StatusId { get; set; }
    }
}