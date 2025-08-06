using AppAPI.Services.BaseServices;
using AppDB.Models.DtoAndViewModels.BaseServices.Common;
using AppDB.Models.DtoAndViewModels.ComboDetailsService.Dto;
using AppDB.Models.DtoAndViewModels.ComboDetailsService.ViewModels;
using AppDB.Models.Entity;

namespace AppAPI.Services.ComboDetailsService
{
    public interface IComboDetailsService : IBaseService<ComboDetails>
    {
        Task<PagedList<ComboDetailsDto>> GetData(ComboDetailsSearch search);
        Task<ComboDetailsDto> GetDto(Guid id);
    }
} 