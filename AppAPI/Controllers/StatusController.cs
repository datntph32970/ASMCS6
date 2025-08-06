using AppAPI.Attributes;
using AppAPI.Services.StatusService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AppDB.Models.Entity;
using AppDB.Models.DtoAndViewModels.StatusService.ViewModels;
using AppDB.Models.DtoAndViewModels.StatusService.Dto;
using AppAPI.Services.MapperService;

namespace AppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;
        private readonly IMapper _mapper;

        public StatusController(IStatusService statusService, IMapper mapper)
        {
            _statusService = statusService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetStatuses([FromQuery] StatusSearch search)
        {
            var result = await _statusService.GetData(search);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStatus(Guid id)
        {
            var result = await _statusService.GetDto(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [JwtAuthorize("Admin", "Staff")]
        public async Task<IActionResult> CreateStatus([FromBody] StatusCreateVM request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var status = _mapper.Map<StatusCreateVM,Status>(request);
            await _statusService.CreateAsync(status);
            return CreatedAtAction(nameof(GetStatus), new { id = status.id }, status);
        }

        [HttpPut]
        [JwtAuthorize("Admin", "Staff")]
        public async Task<IActionResult> UpdateStatus([FromBody] StatusUpdateVM request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var status = await _statusService.GetByIdAsync(request.id);
            if (status == null)
                return NotFound();
            _mapper.Map(request, status);
            await _statusService.UpdateAsync(status);
            return Ok(status);
        }

        [HttpDelete("{id}")]
        [JwtAuthorize("Admin")]
        public async Task<IActionResult> DeleteStatus(Guid id)
        {
            var status = await _statusService.GetByIdAsync(id);
            if (status == null)
                return NotFound();
            await _statusService.DeleteAsync(status);
            return NoContent();
        }
    }
}