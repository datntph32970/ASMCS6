using AppAPI.Services.BaseServices;
using AppDB.Models.DtoAndViewModels.BaseServices.Common;
using AppDB.Models.DtoAndViewModels.CombosService.Dto;
using AppDB.Models.DtoAndViewModels.CombosService.ViewModels;
using AppDB.Models.Entity;

namespace AppAPI.Services.CombosService
{
    public interface ICombosService : IBaseService<Combos>
    {
        Task<PagedList<CombosDto>> GetData(CombosSearch search);
        Task<CombosDto> GetDto(Guid id);
    }
} 