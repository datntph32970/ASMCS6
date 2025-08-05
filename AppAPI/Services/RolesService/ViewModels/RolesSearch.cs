using AppAPI.Services.BaseServices.Common;

namespace AppAPI.Services.RolesService.ViewModels
{
    public class RolesSearch:SearchBase
    {
        public string? RoleName { get; set; }
    }
}
