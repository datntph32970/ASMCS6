using AppAPI.Attributes;
using AppAPI.Services.StatusOrdersService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AppDB.Models.Entity;
using AppDB.Models.DtoAndViewModels.StatusOrdersService.ViewModels;
using AppDB.Models.DtoAndViewModels.StatusOrdersService.Dto;
using AppAPI.Services.MapperService;

namespace AppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StatusOrdersController : ControllerBase
    {
        private readonly IStatusOrdersService _statusOrdersService;
        private readonly IMapper _mapper;

        public StatusOrdersController(IStatusOrdersService statusOrdersService, IMapper mapper)
        {
            _statusOrdersService = statusOrdersService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetStatusOrders([FromQuery] StatusOrdersSearch search)
        {
            var result = await _statusOrdersService.GetData(search);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStatusOrder(Guid id)
        {
            var result = await _statusOrdersService.GetDto(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [JwtAuthorize("Admin", "Staff")]
        public async Task<IActionResult> CreateStatusOrder([FromBody] StatusOrdersCreateVM request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var statusOrder = _mapper.Map<StatusOrdersCreateVM,StatusOrders>(request);
            await _statusOrdersService.CreateAsync(statusOrder);
            return CreatedAtAction(nameof(GetStatusOrder), new { id = statusOrder.id }, statusOrder);
        }

        [HttpPut]
        [JwtAuthorize("Admin", "Staff")]
        public async Task<IActionResult> UpdateStatusOrder([FromBody] StatusOrdersUpdateVM request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var statusOrder = await _statusOrdersService.GetByIdAsync(request.id);
            if (statusOrder == null)
                return NotFound();
            _mapper.Map(request, statusOrder);
            await _statusOrdersService.UpdateAsync(statusOrder);
            return Ok(statusOrder);
        }

        [HttpDelete("{id}")]
        [JwtAuthorize("Admin")]
        public async Task<IActionResult> DeleteStatusOrder(Guid id)
        {
            var statusOrder = await _statusOrdersService.GetByIdAsync(id);
            if (statusOrder == null)
                return NotFound();
            await _statusOrdersService.DeleteAsync(statusOrder);
            return NoContent();
        }
    }
} 