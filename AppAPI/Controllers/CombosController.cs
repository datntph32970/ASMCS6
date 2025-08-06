using AppAPI.Attributes;
using AppAPI.Services.BaseServices;
using AppAPI.Services.CombosService;
using AppAPI.Services.CombosService.Dto;
using AppAPI.Services.CombosService.ViewModels;
using AppDB.Models.Entity;
using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CombosController : ControllerBase
    {
        private readonly ICombosService _combosService;
        private readonly IMapper _mapper;

        public CombosController(ICombosService combosService, IMapper mapper)
        {
            _combosService = combosService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCombos([FromQuery] CombosSearch search)
        {
            var result = await _combosService.GetData(search);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCombo(Guid id)
        {
            var result = await _combosService.GetDto(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [JwtAuthorize("Admin", "Staff")]
        public async Task<IActionResult> CreateCombo([FromForm] CombosCreateVM request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var combo = _mapper.Map<Combos>(request);
            if (request.Image != null)
            {
                var imagePath = UploadFileHelper.UploadFile(request.Image, "combos");
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                combo.ImageURL = imagePath;
            }
            await _combosService.CreateAsync(combo);
            var dto = _mapper.Map<CombosDto>(combo);
            return CreatedAtAction(nameof(GetCombo), new { id = combo.id }, dto);
        }

        [HttpPut]
        [JwtAuthorize("Admin", "Staff")]
        public async Task<IActionResult> UpdateCombo([FromForm] CombosUpdateVM request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var combo = await _combosService.GetByIdAsync(request.Id);
            if (combo == null)
                return NotFound();
            _mapper.Map(request, combo);
            if (request.Image != null)
            {
                var imagePath = UploadFileHelper.UploadFile(request.Image, "combos");
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                combo.ImageURL = imagePath;
            }
            await _combosService.UpdateAsync(combo);
            var dto = _mapper.Map<CombosDto>(combo);
            return Ok(dto);
        }

        [HttpDelete("{id}")]
        [JwtAuthorize("Admin")]
        public async Task<IActionResult> DeleteCombo(Guid id)
        {
            var combo = await _combosService.GetByIdAsync(id);
            if (combo == null)
                return NotFound();
            await _combosService.DeleteAsync(combo);
            return NoContent();
        }
    }
}