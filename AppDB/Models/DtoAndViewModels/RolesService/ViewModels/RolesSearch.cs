
using AppDB.Models.DtoAndViewModels.BaseServices.Common;

namespace AppDB.Models.DtoAndViewModels.RolesService.ViewModels
{
    public class RolesSearch:SearchBase
    {
        public string? RoleName { get; set; }
    }
}
