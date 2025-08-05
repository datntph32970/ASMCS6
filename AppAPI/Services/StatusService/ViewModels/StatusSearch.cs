using AppAPI.Services.BaseServices.Common;
using System.ComponentModel.DataAnnotations;

namespace AppAPI.Services.StatusService.ViewModels
{
    public class StatusSearch : SearchBase
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}