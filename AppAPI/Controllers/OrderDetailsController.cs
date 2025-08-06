using AppAPI.Attributes;
using AppAPI.Services.OrderDetailsService;
using AppAPI.Services.OrderDetailsService.Dto;
using AppAPI.Services.OrderDetailsService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using AppDB.Models.Entity;

namespace AppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailsService _orderDetailsService;
        private readonly IMapper _mapper;

        public OrderDetailsController(IOrderDetailsService orderDetailsService, IMapper mapper)
        {
            _orderDetailsService = orderDetailsService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderDetails([FromQuery] OrderDetailsSearch search)
        {
            var result = await _orderDetailsService.GetData(search);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetail(Guid id)
        {
            var result = await _orderDetailsService.GetDto(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [JwtAuthorize("Admin", "Staff")]
        public async Task<IActionResult> CreateOrderDetail([FromBody] OrderDetailsCreateVM request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var detail = _mapper.Map<OrderDetails>(request);
            await _orderDetailsService.CreateAsync(detail);
            var dto = _mapper.Map<OrderDetailsDto>(detail);
            return CreatedAtAction(nameof(GetOrderDetail), new { id = detail.id }, dto);
        }

        [HttpPut]
        [JwtAuthorize("Admin", "Staff")]
        public async Task<IActionResult> UpdateOrderDetail([FromBody] OrderDetailsUpdateVM request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var detail = await _orderDetailsService.GetByIdAsync(request.Id);
            if (detail == null)
                return NotFound();
            _mapper.Map(request, detail);
            await _orderDetailsService.UpdateAsync(detail);
            var dto = _mapper.Map<OrderDetailsDto>(detail);
            return Ok(dto);
        }

        [HttpDelete("{id}")]
        [JwtAuthorize("Admin")]
        public async Task<IActionResult> DeleteOrderDetail(Guid id)
        {
            var detail = await _orderDetailsService.GetByIdAsync(id);
            if (detail == null)
                return NotFound();
            await _orderDetailsService.DeleteAsync(detail);
            return NoContent();
        }
    }
}