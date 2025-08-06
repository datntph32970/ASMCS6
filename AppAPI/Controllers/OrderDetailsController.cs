using AppAPI.Attributes;
using AppAPI.Services.OrderDetailsService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AppDB.Models.Entity;
using AppDB.Models.DtoAndViewModels.OrderDetailsService.ViewModels;
using AppDB.Models.DtoAndViewModels.OrderDetailsService.Dto;
using AppAPI.Services.MapperService;

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
            var detail = _mapper.Map<OrderDetailsCreateVM,OrderDetails>(request);
            await _orderDetailsService.CreateAsync(detail);
            return CreatedAtAction(nameof(GetOrderDetail), new { id = detail.id }, detail);
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
            return Ok(detail);
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