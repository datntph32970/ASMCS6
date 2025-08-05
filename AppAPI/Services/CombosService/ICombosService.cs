using AppAPI.Services.BaseServices;
using AppAPI.Services.BaseServices.Common;
using AppAPI.Services.CombosService.Dto;
using AppAPI.Services.CombosService.ViewModels;
using AppDB.Models;

namespace AppAPI.Services.CombosService
{
    public interface ICombosService : IBaseService<Combos>
    {
        Task<PagedList<CombosDto>> GetData(CombosSearch search);
        Task<CombosDto> GetDto(Guid id);
    }
} 