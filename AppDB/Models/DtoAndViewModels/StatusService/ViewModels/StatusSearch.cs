using AppDB.Models.DtoAndViewModels.BaseServices.Common;
using System.ComponentModel.DataAnnotations;

namespace AppDB.Models.DtoAndViewModels.StatusService.ViewModels
{
    public class StatusSearch : SearchBase
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}