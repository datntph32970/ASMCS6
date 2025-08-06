using AppAPI.Repositories.BaseRepository;
using AppAPI.Services.BaseServices;
using AppAPI.Services.BaseServices.Common;
using AppAPI.Services.OrderDetailsService.Dto;
using AppAPI.Services.OrderDetailsService.ViewModels;
using AppDB.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Services.OrderDetailsService
{
    public class OrderDetailsService : BaseService<OrderDetails>, IOrderDetailsService
    {
        public OrderDetailsService(IBaseRepository<OrderDetails> repository) : base(repository)
        {
        }

        public async Task<PagedList<OrderDetailsDto>> GetData(OrderDetailsSearch search)
        {
            var query = from od in GetQueryable()
                        select new OrderDetailsDto
                        {
                            id = od.id,
                            OrderID = od.OrderID,
                            ProductID = od.ProductID,
                            ComboID = od.ComboID,
                            Quantity = od.Quantity,
                            UnitPrice = od.UnitPrice,
                            Order = od.Order,
                            Product = od.Product,
                            createdById = od.createdById,
                            createdByName = od.createdByName,
                            createdDate = od.createdDate,
                            updatedById = od.updatedById,
                            updatedByName = od.updatedByName,
                            updatedDate = od.updatedDate
                        };
            if(search != null)
            {
                if(search.OrderID.HasValue)
                    query = query.Where(x => x.OrderID == search.OrderID);
                if(search.ProductID.HasValue)
                    query = query.Where(x => x.ProductID == search.ProductID);
                if(search.ComboID.HasValue)
                    query = query.Where(x => x.ComboID == search.ComboID);
            }
            query = query.OrderByDescending(x => x.createdDate);
            return await PagedList<OrderDetailsDto>.CreateAsync(query, search);
        }

        public async Task<OrderDetailsDto> GetDto(Guid id)
        {
            var item = await (from od in GetQueryable().Where(x => x.id == id)
                              select new OrderDetailsDto
                              {
                                  id = od.id,
                                  OrderID = od.OrderID,
                                  ProductID = od.ProductID,
                                    ComboID = od.ComboID,
                                  Quantity = od.Quantity,
                                  UnitPrice = od.UnitPrice,
                                  Order = od.Order,
                                  Product = od.Product,
                                  createdById = od.createdById,
                                  createdByName = od.createdByName,
                                  createdDate = od.createdDate,
                                  updatedById = od.updatedById,
                                  updatedByName = od.updatedByName,
                                  updatedDate = od.updatedDate
                              }).FirstOrDefaultAsync();

            return item;
        }
    }
} 