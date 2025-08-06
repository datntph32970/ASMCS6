using AppDB.Models.DtoAndViewModels.BaseServices.Common;
using System.ComponentModel.DataAnnotations;

namespace AppDB.Models.DtoAndViewModels.StatusOrdersService.ViewModels
{
    public class StatusOrdersSearch:SearchBase
    {
        public Guid? OrderID { get; set; }
        public Guid? StatusID { get; set; }
    }
}