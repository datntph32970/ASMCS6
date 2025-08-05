using AppAPI.Services.BaseServices.Common;
using System.ComponentModel.DataAnnotations;

namespace AppAPI.Services.StatusOrdersService.ViewModels
{
    public class StatusOrdersSearch:SearchBase
    {
        public Guid? OrderID { get; set; }
        public Guid? StatusID { get; set; }
    }
}