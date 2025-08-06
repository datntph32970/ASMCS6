using System.ComponentModel.DataAnnotations;

namespace AppDB.Models.DtoAndViewModels.StatusService.ViewModels
{
    public class StatusUpdateVM: StatusCreateVM
    {
        public Guid id { get; set; }
    }
}