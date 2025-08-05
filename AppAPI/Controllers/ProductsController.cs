using AppAPI.Attributes;
using AppAPI.Services.ProductsService;
using AppAPI.Services.ProductsService.Dto;
using AppAPI.Services.ProductsService.ViewModels;
using AppAPI.Services.BaseServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

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
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var result = await _productsService.GetDto(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [JwtAuthorize("Admin", "Staff")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductsCreateVM request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var product = _mapper.Map<AppDB.Models.Products>(request);
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
            var dto = _mapper.Map<ProductsDto>(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.id }, dto);
        }

        [HttpPut("{id}")]
        [JwtAuthorize("Admin", "Staff")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromForm] ProductsUpdateVM request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _productsService.GetByIdAsync(id);
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
            var dto = _mapper.Map<ProductsDto>(product);
            return Ok(dto);
        }

        [HttpDelete("{id}")]
        [JwtAuthorize("Admin")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _productsService.GetByIdAsync(id);
            if (product == null)
                return NotFound();
            await _productsService.DeleteAsync(product);
            return NoContent();
        }
    }
}