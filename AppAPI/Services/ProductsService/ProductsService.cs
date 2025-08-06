using AppAPI.Repositories.BaseRepository;
using AppAPI.Services.BaseServices;
using AppAPI.Services.BaseServices.Common;
using AppAPI.Services.ProductsService.Dto;
using AppAPI.Services.ProductsService.ViewModels;
using AppDB.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Services.ProductsService
{
    public class ProductsService : BaseService<Products>, IProductsService
    {
        public ProductsService(IBaseRepository<Products> repository) : base(repository)
        {
        }

        public async Task<PagedList<ProductsDto>> GetData(ProductsSearch search)
        {
            var query = from p in GetQueryable()
                        select new ProductsDto
                        {
                            id = p.id,
                            ProductName = p.ProductName,
                            Description = p.Description,
                            Price = p.Price,
                            ImageURL = p.ImageURL,
                            CategoryID = p.CategoryID,
                            Category = p.Category,
                            OrderDetails = p.OrderDetails,
                            ComboDetails = p.ComboDetails,
                            createdById = p.createdById,
                            createdByName = p.createdByName,
                            createdDate = p.createdDate,
                            updatedById = p.updatedById,
                            updatedByName = p.updatedByName,
                            updatedDate = p.updatedDate
                        };
            if(search != null)
            {
                if(!string.IsNullOrEmpty(search.ProductName))
                    query = query.Where(x => x.ProductName.Contains(search.ProductName));
                if(!string.IsNullOrEmpty(search.Description))
                    query = query.Where(x => x.Description.Contains(search.Description));
                if(search.CategoryID.HasValue)
                    query = query.Where(x => x.CategoryID == search.CategoryID);
            }
            query = query.OrderByDescending(x => x.createdDate);
            return await PagedList<ProductsDto>.CreateAsync(query, search);
        }

        public async Task<ProductsDto> GetDto(Guid id)
        {
            var item = await (from p in GetQueryable().Where(x => x.id == id)
                              select new ProductsDto
                              {
                                  id = p.id,
                                  ProductName = p.ProductName,
                                  Description = p.Description,
                                  Price = p.Price,
                                  ImageURL = p.ImageURL,
                                  CategoryID = p.CategoryID,
                                  Category = p.Category,
                                  OrderDetails = p.OrderDetails,
                                  ComboDetails = p.ComboDetails,
                                  createdById = p.createdById,
                                  createdByName = p.createdByName,
                                  createdDate = p.createdDate,
                                  updatedById = p.updatedById,
                                  updatedByName = p.updatedByName,
                                  updatedDate = p.updatedDate
                              }).FirstOrDefaultAsync();

            return item;
        }
    }
} 