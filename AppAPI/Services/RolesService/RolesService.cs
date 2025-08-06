using AppAPI.Repositories.BaseRepository;
using AppAPI.Services.BaseServices;
using AppAPI.Services.BaseServices.Common;
using AppAPI.Services.RolesService.Dto;
using AppAPI.Services.RolesService.ViewModels;
using AppDB.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Services.RolesService
{
    public class RolesService : BaseService<Roles>, IRolesService
    {
        public RolesService(IBaseRepository<Roles> repository) : base(repository)
        {
        }

        public async Task<PagedList<RolesDto>> GetData(RolesSearch search)
        {
            var query = from r in GetQueryable()
                        select new RolesDto
                        {
                            id = r.id,
                            RoleName = r.RoleName,
                            createdById = r.createdById,
                            createdByName = r.createdByName,
                            createdDate = r.createdDate,
                            updatedById = r.updatedById,
                            updatedByName = r.updatedByName,
                            updatedDate = r.updatedDate
                        };
            if(search != null)
                if(!string.IsNullOrEmpty(search.RoleName))
                    query = query.Where(x => x.RoleName == search.RoleName);
            query = query.OrderByDescending(x => x.createdDate);
            return  await PagedList<RolesDto>.CreateAsync(query, search);
        }

        public async Task<RolesDto> GetDto(Guid id)
        {
            var item = await (from r in GetQueryable().Where(x => x.id == id)
                              select new RolesDto
                              {
                                  id = r.id,
                                  RoleName = r.RoleName,
                                  createdById = r.createdById,
                                  createdByName = r.createdByName,
                                  createdDate = r.createdDate,
                                  updatedById = r.updatedById,
                                  updatedByName = r.updatedByName,
                                  updatedDate = r.updatedDate
                              }).FirstOrDefaultAsync();

            return item ;
        }
    }
}
