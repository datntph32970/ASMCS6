using AppAPI.Attributes;
using AppAPI.Services.CategoriesService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AppDB.Models.Entity;
using AppDB.Models.DtoAndViewModels.CategoriesService.ViewModels;
using AppDB.Models.DtoAndViewModels.CategoriesService.Dto;
using AppAPI.Services.MapperService;

namespace AppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoriesService categoriesService, IMapper mapper)
        {
            _categoriesService = categoriesService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] CategoriesSearch search)
        {
            var result = await _categoriesService.GetData(search);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            var result = await _categoriesService.GetDto(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [JwtAuthorize("Admin", "Staff")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoriesCreateVM request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var category = _mapper.Map<CategoriesCreateVM,Categories>(request);
            await _categoriesService.CreateAsync(category);
            return CreatedAtAction(nameof(GetCategory), new { id = category.id }, category);
        }

        [HttpPut]
        [JwtAuthorize("Admin", "Staff")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoriesUpdateVM request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var category = await _categoriesService.GetByIdAsync(request.Id);
            if (category == null)
                return NotFound();
            _mapper.Map(request, category);
            await _categoriesService.UpdateAsync(category);
            return Ok(category);
        }

        [HttpDelete("{id}")]
        [JwtAuthorize("Admin")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _categoriesService.GetByIdAsync(id);
            if (category == null)
                return NotFound();
            await _categoriesService.DeleteAsync(category);
            return NoContent();
        }
    }
}