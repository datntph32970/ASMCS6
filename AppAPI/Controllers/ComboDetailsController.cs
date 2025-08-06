using AppAPI.Attributes;
using AppAPI.Services.ComboDetailsService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AppDB.Models.Entity;
using AppDB.Models.DtoAndViewModels.ComboDetailsService.ViewModels;
using AppDB.Models.DtoAndViewModels.ComboDetailsService.Dto;
using AppAPI.Services.MapperService;

namespace AppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ComboDetailsController : ControllerBase
    {
        private readonly IComboDetailsService _comboDetailsService;
        private readonly IMapper _mapper;

        public ComboDetailsController(IComboDetailsService comboDetailsService, IMapper mapper)
        {
            _comboDetailsService = comboDetailsService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetComboDetails([FromQuery] ComboDetailsSearch search)
        {
            var result = await _comboDetailsService.GetData(search);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComboDetail(Guid id)
        {
            var result = await _comboDetailsService.GetDto(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [JwtAuthorize("Admin", "Staff")]
        public async Task<IActionResult> CreateComboDetail([FromBody] ComboDetailsCreateVM request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var detail = _mapper.Map<ComboDetailsCreateVM,ComboDetails>(request);
            await _comboDetailsService.CreateAsync(detail);
            return CreatedAtAction(nameof(GetComboDetail), new { id = detail.id }, detail);
        }

        [HttpPut]
        [JwtAuthorize("Admin", "Staff")]
        public async Task<IActionResult> UpdateComboDetail([FromBody] ComboDetailsUpdateVM request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var detail = await _comboDetailsService.GetByIdAsync(request.Id);
            if (detail == null)
                return NotFound();
            _mapper.Map(request, detail);
            await _comboDetailsService.UpdateAsync(detail);
            return Ok(detail);
        }

        [HttpDelete("{id}")]
        [JwtAuthorize("Admin")]
        public async Task<IActionResult> DeleteComboDetail(Guid id)
        {
            var detail = await _comboDetailsService.GetByIdAsync(id);
            if (detail == null)
                return NotFound();
            await _comboDetailsService.DeleteAsync(detail);
            return NoContent();
        }
    }
}