using System.ComponentModel.DataAnnotations;

namespace AppDB.Models.DtoAndViewModels.CategoriesService.ViewModels
{
    public class CategoriesUpdateVM : CategoriesCreateVM
    {
        [Required(ErrorMessage = "ID là bắt buộc")]
        public Guid Id { get; set; }
    }
}