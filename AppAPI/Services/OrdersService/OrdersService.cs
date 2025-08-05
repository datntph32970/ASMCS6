using AppAPI.Repositories.BaseRepository;
using AppAPI.Services.BaseServices;
using AppAPI.Services.BaseServices.Common;
using AppAPI.Services.OrdersService.Dto;
using AppAPI.Services.OrdersService.ViewModels;
using AppDB.Models;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Services.OrdersService
{
    public class OrdersService : BaseService<Orders>, IOrdersService
    {
        public OrdersService(IBaseRepository<Orders> repository) : base(repository)
        {
        }

        public async Task<PagedList<OrdersDto>> GetData(OrdersSearch search)
        {
            var query = from o in GetQueryable()
                        select new OrdersDto
                        {
                            id = o.id,
                            CustomerID = o.CustomerID,
                            StaffID = o.StaffID,
                            OrderDate = o.OrderDate,
                            TotalAmount = o.TotalAmount,
                            Customer = o.Customer,
                            Staff = o.Staff,
                            OrderDetails = o.OrderDetails,
                            createdById = o.createdById,
                            createdByName = o.createdByName,
                            createdDate = o.createdDate,
                            updatedById = o.updatedById,
                            updatedByName = o.updatedByName,
                            updatedDate = o.updatedDate
                        };
            if(search != null)
            {
                if(search.CustomerID.HasValue)
                    query = query.Where(x => x.CustomerID == search.CustomerID);
                if(search.StaffID.HasValue)
                    query = query.Where(x => x.StaffID == search.StaffID);
                
                if(search.FromDate.HasValue)
                    query = query.Where(x => x.OrderDate >= search.FromDate);
                if(search.ToDate.HasValue)
                    query = query.Where(x => x.OrderDate <= search.ToDate);
            }
            query = query.OrderByDescending(x => x.OrderDate);
            return await PagedList<OrdersDto>.CreateAsync(query, search);
        }

        public async Task<OrdersDto> GetDto(Guid id)
        {
            var item = await (from o in GetQueryable().Where(x => x.id == id)
                              select new OrdersDto
                              {
                                  id = o.id,
                                  CustomerID = o.CustomerID,
                                  StaffID = o.StaffID,
                                  OrderDate = o.OrderDate,
                                  TotalAmount = o.TotalAmount,
                                  Customer = o.Customer,
                                  Staff = o.Staff,
                                  OrderDetails = o.OrderDetails,
                                  createdById = o.createdById,
                                  createdByName = o.createdByName,
                                  createdDate = o.createdDate,
                                  updatedById = o.updatedById,
                                  updatedByName = o.updatedByName,
                                  updatedDate = o.updatedDate
                              }).FirstOrDefaultAsync();

            return item;
        }
    }
} 