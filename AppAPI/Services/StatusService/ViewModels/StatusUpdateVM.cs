using System.ComponentModel.DataAnnotations;

namespace AppAPI.Services.StatusService.ViewModels
{
    public class StatusUpdateVM: StatusCreateVM
    {
        public Guid id { get; set; }
    }
}