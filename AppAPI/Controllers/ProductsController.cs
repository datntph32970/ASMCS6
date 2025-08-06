using AppAPI.Attributes;
using AppAPI.Services.ProductsService;
using AppAPI.Services.BaseServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AppDB.Models.Entity;
using AppDB.Models.DtoAndViewModels.ProductsService.ViewModels;
using AppDB.Models.DtoAndViewModels.ProductsService.Dto;
using AppAPI.Services.MapperService;
using AppDB.Models.DtoAndViewModels.BaseServices.Common;
using AppDB.Models.DtoAndViewModels.AuthService.ViewModels;

namespace AppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;
        private readonly IMapper _mapper;

        public ProductsController(IProductsService productsService, IMapper mapper)
        {
            _productsService = productsService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] ProductsSearch search)
        {
            var result = await _productsService.GetData(search);
            return Ok(ApiResponse<PagedList<ProductsDto>>.Ok(result, "Lấy danh sách sản phẩm thành công"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var result = await _productsService.GetDto(id);
            if (result == null)
                return NotFound();
            return Ok(ApiResponse<ProductsDto>.Ok(result, "Lấy thông tin sản phẩm thành công"));
        }

        [HttpPost]
        [JwtAuthorize("Admin", "Staff")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductsCreateVM request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<ProductsCreateVM>.Error("Dữ liệu không hợp lệ"));

            var product = _mapper.Map<ProductsCreateVM,Products>(request);
            if (request.Image != null)
            {
                var imagePath = UploadFileHelper.UploadFile(request.Image, "products");
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                product.ImageURL = imagePath;
            }

            await _productsService.CreateAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.id }, product);
        }

        [HttpPut]
        [JwtAuthorize("Admin", "Staff")]
        public async Task<IActionResult> UpdateProduct( [FromForm] ProductsUpdateVM request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<ProductsUpdateVM>.Error("Dữ liệu không hợp lệ"));

            var product = await _productsService.GetByIdAsync(request.Id);
            if (product == null)
                return NotFound();

            _mapper.Map(request, product);
            if (request.Image != null)
            {
                var imagePath = UploadFileHelper.UploadFile(request.Image, "products");
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                product.ImageURL = imagePath;
            }
            await _productsService.UpdateAsync(product);
            return Ok(ApiResponse<ProductsUpdateVM>.Ok(request, "Cập nhật sản phẩm thành công"));
        }

        [HttpDelete("{id}")]
        [JwtAuthorize("Admin")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _productsService.GetByIdAsync(id);
            if (product == null)
                return NotFound();
            await _productsService.DeleteAsync(product);
            return Ok(ApiResponse<string>.Ok("Xóa sản phẩm thành công", "Sản phẩm đã được xóa thành công"));
        }
    }
}