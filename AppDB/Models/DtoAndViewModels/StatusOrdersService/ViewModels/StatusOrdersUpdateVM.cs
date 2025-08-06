using System.ComponentModel.DataAnnotations;

namespace AppDB.Models.DtoAndViewModels.StatusOrdersService.ViewModels
{
    public class StatusOrdersUpdateVM: StatusOrdersCreateVM
    {
        public Guid id { get; set; }
    }
}