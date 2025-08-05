using AppAPI.Repositories.StatusOrdersRepository;
using AppAPI.Services.BaseServices;
using AppAPI.Services.BaseServices.Common;
using AppAPI.Services.StatusOrdersService.Dto;
using AppAPI.Services.StatusOrdersService.ViewModels;
using AppDB.Models;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Services.StatusOrdersService
{
    public class StatusOrdersService : BaseService<StatusOrders>, IStatusOrdersService
    {
        public StatusOrdersService(IStatusOrdersRepository repository) : base(repository)
        {
        }

        public async Task<PagedList<StatusOrdersDto>> GetData(StatusOrdersSearch search)
        {
            var query = from so in GetQueryable()
                        select new StatusOrdersDto
                        {
                            id = so.id,
                            OrderId = so.OrderId,
                            StatuId = so.StatuId,
                            createdById = so.createdById,
                            createdByName = so.createdByName,
                            createdDate = so.createdDate,
                            updatedById = so.updatedById,
                            updatedByName = so.updatedByName,
                            updatedDate = so.updatedDate
                        };
            if (search != null)
            {
                if (search.OrderID.HasValue)
                    query = query.Where(x => x.OrderId == search.OrderID);
                if (search.StatusID.HasValue)
                    query = query.Where(x => x.StatuId == search.StatusID);
            }
            query = query.OrderByDescending(x => x.createdDate);
            return await PagedList<StatusOrdersDto>.CreateAsync(query, search);
        }

        public async Task<StatusOrdersDto?> GetDto(Guid id)
        {
            var statusOrder = await GetByIdAsync(id);
            if (statusOrder == null) return null;

            return new StatusOrdersDto
            {
                id = statusOrder.id,
                OrderId = statusOrder.OrderId,
                StatuId = statusOrder.StatuId,
                createdById = statusOrder.createdById,
                createdByName = statusOrder.createdByName,
                createdDate = statusOrder.createdDate,
                updatedById = statusOrder.updatedById,
                updatedByName = statusOrder.updatedByName,
                updatedDate = statusOrder.updatedDate
            };
        }
    }
}