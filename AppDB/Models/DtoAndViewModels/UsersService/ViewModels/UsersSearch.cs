using AppDB.Models.DtoAndViewModels.BaseServices.Common;

namespace AppDB.Models.DtoAndViewModels.UsersService.ViewModels
{
    public class UsersSearch : SearchBase
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
} 