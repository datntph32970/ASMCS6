using AppAPI.Repositories.BaseRepository;
using AppAPI.Services.BaseServices;
using AppDB.Models.DtoAndViewModels.BaseServices.Common;
using AppDB.Models.DtoAndViewModels.ComboDetailsService.Dto;
using AppDB.Models.DtoAndViewModels.ComboDetailsService.ViewModels;
using AppDB.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Services.ComboDetailsService
{
    public class ComboDetailsService : BaseService<ComboDetails>, IComboDetailsService
    {
        public ComboDetailsService(IBaseRepository<ComboDetails> repository) : base(repository)
        {
        }

        public async Task<PagedList<ComboDetailsDto>> GetData(ComboDetailsSearch search)
        {
            var query = from cd in GetQueryable()
                        select new ComboDetailsDto
                        {
                            id = cd.id,
                            ComboID = cd.ComboID,
                            ProductID = cd.ProductID,
                            Quantity = cd.Quantity,
                            Combo = cd.Combo,
                            Product = cd.Product,
                            createdById = cd.createdById,
                            createdByName = cd.createdByName,
                            createdDate = cd.createdDate,
                            updatedById = cd.updatedById,
                            updatedByName = cd.updatedByName,
                            updatedDate = cd.updatedDate
                        };
            if(search != null)
            {
                if(search.ComboID.HasValue)
                    query = query.Where(x => x.ComboID == search.ComboID);
                if(search.ProductID.HasValue)
                    query = query.Where(x => x.ProductID == search.ProductID);
            }
            query = query.OrderByDescending(x => x.createdDate);
            return await PagedList<ComboDetailsDto>.CreateAsync(query, search);
        }

        public async Task<ComboDetailsDto> GetDto(Guid id)
        {
            var item = await (from cd in GetQueryable().Where(x => x.id == id)
                              select new ComboDetailsDto
                              {
                                  id = cd.id,
                                  ComboID = cd.ComboID,
                                  ProductID = cd.ProductID,
                                  Quantity = cd.Quantity,
                                  Combo = cd.Combo,
                                  Product = cd.Product,
                                  createdById = cd.createdById,
                                  createdByName = cd.createdByName,
                                  createdDate = cd.createdDate,
                                  updatedById = cd.updatedById,
                                  updatedByName = cd.updatedByName,
                                  updatedDate = cd.updatedDate
                              }).FirstOrDefaultAsync();

            return item;
        }
    }
} 