using AppAPI.Repositories.BaseRepository;
using AppAPI.Services.BaseServices;
using AppAPI.Services.BaseServices.Common;
using AppAPI.Services.CombosService.Dto;
using AppAPI.Services.CombosService.ViewModels;
using AppDB.Models;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Services.CombosService
{
    public class CombosService : BaseService<Combos>, ICombosService
    {
        public CombosService(IBaseRepository<Combos> repository) : base(repository)
        {
        }

        public async Task<PagedList<CombosDto>> GetData(CombosSearch search)
        {
            var query = from c in GetQueryable()
                        select new CombosDto
                        {
                            id = c.id,
                            ComboName = c.ComboName,
                            Description = c.Description,
                            Price = c.Price,
                            ImageURL = c.ImageURL,
                            ComboDetails = c.ComboDetails,
                            createdById = c.createdById,
                            createdByName = c.createdByName,
                            createdDate = c.createdDate,
                            updatedById = c.updatedById,
                            updatedByName = c.updatedByName,
                            updatedDate = c.updatedDate
                        };
            if(search != null)
            {
                if(!string.IsNullOrEmpty(search.ComboName))
                    query = query.Where(x => x.ComboName.Contains(search.ComboName));
                if(!string.IsNullOrEmpty(search.Description))
                    query = query.Where(x => x.Description.Contains(search.Description));
            }
            query = query.OrderByDescending(x => x.createdDate);
            return await PagedList<CombosDto>.CreateAsync(query, search);
        }

        public async Task<CombosDto> GetDto(Guid id)
        {
            var item = await (from c in GetQueryable().Where(x => x.id == id)
                              select new CombosDto
                              {
                                  id = c.id,
                                  ComboName = c.ComboName,
                                  Description = c.Description,
                                  Price = c.Price,
                                  ImageURL = c.ImageURL,
                                  ComboDetails = c.ComboDetails,
                                  createdById = c.createdById,
                                  createdByName = c.createdByName,
                                  createdDate = c.createdDate,
                                  updatedById = c.updatedById,
                                  updatedByName = c.updatedByName,
                                  updatedDate = c.updatedDate
                              }).FirstOrDefaultAsync();

            return item;
        }
    }
} 