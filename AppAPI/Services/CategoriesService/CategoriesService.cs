using AppAPI.Repositories.BaseRepository;
using AppAPI.Services.BaseServices;
using AppAPI.Services.BaseServices.Common;
using AppAPI.Services.CategoriesService.Dto;
using AppAPI.Services.CategoriesService.ViewModels;
using AppDB.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Services.CategoriesService
{
    public class CategoriesService : BaseService<Categories>, ICategoriesService
    {
        public CategoriesService(IBaseRepository<Categories> repository) : base(repository)
        {
        }

        public async Task<PagedList<CategoriesDto>> GetData(CategoriesSearch search)
        {
            var query = from c in GetQueryable()
                        select new CategoriesDto
                        {
                            id = c.id,
                            CategoryName = c.CategoryName,
                            Description = c.Description,
                            Products = c.Products,
                            createdById = c.createdById,
                            createdByName = c.createdByName,
                            createdDate = c.createdDate,
                            updatedById = c.updatedById,
                            updatedByName = c.updatedByName,
                            updatedDate = c.updatedDate
                        };
            if(search != null)
            {
                if(!string.IsNullOrEmpty(search.CategoryName))
                    query = query.Where(x => x.CategoryName.Contains(search.CategoryName));
                if(!string.IsNullOrEmpty(search.Description))
                    query = query.Where(x => x.Description.Contains(search.Description));
            }
            query = query.OrderByDescending(x => x.createdDate);
            return await PagedList<CategoriesDto>.CreateAsync(query, search);
        }

        public async Task<CategoriesDto> GetDto(Guid id)
        {
            var item = await (from c in GetQueryable().Where(x => x.id == id)
                              select new CategoriesDto
                              {
                                  id = c.id,
                                  CategoryName = c.CategoryName,
                                  Description = c.Description,
                                  Products = c.Products,
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