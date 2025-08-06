using AppAPI.Attributes;
using AppAPI.Services.OrdersService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AppDB.Models.Entity;
using AppDB.Models.DtoAndViewModels.OrdersService.ViewModels;
using AppDB.Models.DtoAndViewModels.OrdersService.Dto;
using AppAPI.Services.MapperService;
using AppDB.Models.DtoAndViewModels.BaseServices.Common;
using AppDB.Models.DtoAndViewModels.AuthService.ViewModels;

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
            return Ok(ApiResponse<PagedList<OrdersDto>>.Ok(result, "Lấy danh sách đơn hàng thành công"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            var result = await _ordersService.GetDto(id);
            if (result == null)
                return NotFound();
            return Ok(ApiResponse<OrdersDto>.Ok(result, "Lấy thông tin đơn hàng thành công"));
        }

        [HttpPost]
        [JwtAuthorize("Admin", "Staff")]
        public async Task<IActionResult> CreateOrder([FromBody] OrdersCreateVM request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<OrdersCreateVM>.Error("Dữ liệu không hợp lệ"));
            var order = _mapper.Map<OrdersCreateVM,Orders>(request);
            await _ordersService.CreateAsync(order);
            return CreatedAtAction(nameof(GetOrder), new { id = order.id }, order);
        }

        [HttpPut]
        [JwtAuthorize("Admin", "Staff")]
        public async Task<IActionResult> UpdateOrder( [FromBody] OrdersUpdateVM request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<OrdersUpdateVM>.Error("Dữ liệu không hợp lệ"));
            var order = await _ordersService.GetByIdAsync(request.Id);
            if (order == null)
                return NotFound();
            _mapper.Map(request, order);
            await _ordersService.UpdateAsync(order);
            return Ok(ApiResponse<OrdersUpdateVM>.Ok(request, "Cập nhật đơn hàng thành công"));
        }

        [HttpDelete("{id}")]
        [JwtAuthorize("Admin")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var order = await _ordersService.GetByIdAsync(id);
            if (order == null)
                return NotFound();
            await _ordersService.DeleteAsync(order);
            return Ok(ApiResponse<string>.Ok(null, "Đơn hàng đã được xóa thành công"));
        }
    }
}