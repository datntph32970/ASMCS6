using AppAPI.Attributes;
using AppAPI.Services.OrdersService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AppDB.Models.Entity;
using AppDB.Models.DtoAndViewModels.OrdersService.ViewModels;
using AppDB.Models.DtoAndViewModels.OrdersService.Dto;
using AppAPI.Services.MapperService;

namespace AppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;
        private readonly IMapper _mapper;

        public OrdersController(IOrdersService ordersService, IMapper mapper)
        {
            _ordersService = ordersService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] OrdersSearch search)
        {
            var result = await _ordersService.GetData(search);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            var result = await _ordersService.GetDto(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [JwtAuthorize("Admin", "Staff")]
        public async Task<IActionResult> CreateOrder([FromBody] OrdersCreateVM request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var order = _mapper.Map<OrdersCreateVM,Orders>(request);
            await _ordersService.CreateAsync(order);
            return CreatedAtAction(nameof(GetOrder), new { id = order.id }, order);
        }

        [HttpPut]
        [JwtAuthorize("Admin", "Staff")]
        public async Task<IActionResult> UpdateOrder( [FromBody] OrdersUpdateVM request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var order = await _ordersService.GetByIdAsync(request.Id);
            if (order == null)
                return NotFound();
            _mapper.Map(request, order);
            await _ordersService.UpdateAsync(order);
            return Ok(order);
        }

        [HttpDelete("{id}")]
        [JwtAuthorize("Admin")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var order = await _ordersService.GetByIdAsync(id);
            if (order == null)
                return NotFound();
            await _ordersService.DeleteAsync(order);
            return NoContent();
        }
    }
}