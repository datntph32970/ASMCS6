using AppAPI.Attributes;
using AppAPI.Services.CategoriesService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AppDB.Models.Entity;
using AppDB.Models.DtoAndViewModels.CategoriesService.ViewModels;
using AppDB.Models.DtoAndViewModels.CategoriesService.Dto;
using AppAPI.Services.MapperService;
using AppDB.Models.DtoAndViewModels.AuthService.ViewModels;
using AppDB.Models.DtoAndViewModels.BaseServices.Common;

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
            return Ok(ApiResponse<PagedList<CategoriesDto>>.Ok(result, "Lấy danh sách danh mục thành công"));    
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            var result = await _categoriesService.GetDto(id);
            if (result == null)
                return NotFound();
            return Ok(ApiResponse<CategoriesDto>.Ok(result, "Lấy thông tin danh mục thành công"));
        }

        [HttpPost]
        [JwtAuthorize("Admin", "Staff")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoriesCreateVM request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<CategoriesCreateVM>.Error("Dữ liệu không hợp lệ"));
            var category = _mapper.Map<CategoriesCreateVM,Categories>(request);
            await _categoriesService.CreateAsync(category);
            return Ok(ApiResponse<Categories>.Ok(category, "Cập nhật danh mục thành công"));
        }

        [HttpPut]
        [JwtAuthorize("Admin", "Staff")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoriesUpdateVM request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<CategoriesCreateVM>.Error("Dữ liệu không hợp lệ"));
            var category = await _categoriesService.GetByIdAsync(request.Id);
            if (category == null)
                return NotFound();
            _mapper.Map(request, category);
            await _categoriesService.UpdateAsync(category);
            return Ok(ApiResponse<CategoriesUpdateVM>.Ok(request, "Cập nhật danh mục thành công"));
        }

        [HttpDelete("{id}")]
        [JwtAuthorize("Admin")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _categoriesService.GetByIdAsync(id);
            if (category == null)
                return NotFound();
            await _categoriesService.DeleteAsync(category);
            return Ok(ApiResponse<string>.Ok(null, "Xóa danh mục thành công"));
        }
    }
}