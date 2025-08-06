using AppAPI.Services.BaseServices;
using AppAPI.Services.BaseServices.Common;
using AppAPI.Services.ComboDetailsService.Dto;
using AppAPI.Services.ComboDetailsService.ViewModels;
using AppDB.Models.Entity;

namespace AppAPI.Services.ComboDetailsService
{
    public interface IComboDetailsService : IBaseService<ComboDetails>
    {
        Task<PagedList<ComboDetailsDto>> GetData(ComboDetailsSearch search);
        Task<ComboDetailsDto> GetDto(Guid id);
    }
} 