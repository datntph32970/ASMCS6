using System.ComponentModel.DataAnnotations;

namespace AppDB.Models.DtoAndViewModels.StatusService.ViewModels
{
    public class StatusCreateVM
    {
        [Required(ErrorMessage = "Tên trạng thái là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên trạng thái không được vượt quá 100 ký tự")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự")]
        public string? Description { get; set; }
    }
}