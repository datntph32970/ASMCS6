using AppAPI.Services.BaseServices.Common;

namespace AppAPI.Services.UsersService.ViewModels
{
    public class UsersSearch : SearchBase
    {
        public string? Username { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
} 