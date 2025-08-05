using System.ComponentModel.DataAnnotations;

namespace AppAPI.Services.StatusOrdersService.ViewModels
{
    public class StatusOrdersUpdateVM: StatusOrdersCreateVM
    {
        public Guid id { get; set; }
    }
}