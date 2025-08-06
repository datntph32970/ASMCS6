using AppAPI.Repositories.StatusRepository;
using AppAPI.Services.BaseServices;
using AppDB.Models.DtoAndViewModels.BaseServices.Common;
using AppDB.Models.DtoAndViewModels.StatusService.Dto;
using AppDB.Models.DtoAndViewModels.StatusService.ViewModels;
using AppDB.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Services.StatusService
{
    public class StatusService : BaseService<Status>, IStatusService
    {
        public StatusService(IStatusRepository repository) : base(repository)
        {
        }

        public async Task<PagedList<StatusDto>> GetData(StatusSearch search)
        {
            var query = from s in GetQueryable()
                        select new StatusDto
                        {
                            id = s.id,
                            Name = s.Name,
                            Description = s.Description,
                            createdById = s.createdById,
                            createdByName = s.createdByName,
                            createdDate = s.createdDate,
                            updatedById = s.updatedById,
                            updatedByName = s.updatedByName,
                            updatedDate = s.updatedDate
                        };
            if (search != null)
            {
                if (!string.IsNullOrEmpty(search.Name))
                    query = query.Where(x => x.Name.Contains(search.Name));
                if (!string.IsNullOrEmpty(search.Description))
                    query = query.Where(x => x.Description.Contains(search.Description));
            }
            query = query.OrderByDescending(x => x.createdDate);
            return await PagedList<StatusDto>.CreateAsync(query, search);
        }

        public async Task<StatusDto?> GetDto(Guid id)
        {
            var status = await GetByIdAsync(id);
            if (status == null) return null;

            return new StatusDto
            {
                id = status.id,
                Name = status.Name,
                Description = status.Description,
                createdById = status.createdById,
                createdByName = status.createdByName,
                createdDate = status.createdDate,
                updatedById = status.updatedById,
                updatedByName = status.updatedByName,
                updatedDate = status.updatedDate
            };
        }
    }
}