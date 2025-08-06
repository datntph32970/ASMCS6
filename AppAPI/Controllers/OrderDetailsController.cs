using AppAPI.Attributes;
using AppAPI.Services.OrderDetailsService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AppDB.Models.Entity;
using AppDB.Models.DtoAndViewModels.OrderDetailsService.ViewModels;
using AppDB.Models.DtoAndViewModels.OrderDetailsService.Dto;
using AppAPI.Services.MapperService;
using AppDB.Models.DtoAndViewModels.AuthService.ViewModels;
using AppDB.Models.DtoAndViewModels.BaseServices.Common;

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
            return Ok(ApiResponse<PagedList<OrderDetailsDto>>.Ok(result, "Lấy danh sách chi tiết đơn hàng thành công"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetail(Guid id)
        {
            var result = await _orderDetailsService.GetDto(id);
            if (result == null)
                return NotFound();
            return Ok(ApiResponse<OrderDetailsDto>.Ok(result, "Lấy thông tin chi tiết đơn hàng thành công"));
        }

        [HttpPost]
        [JwtAuthorize("Admin", "Staff")]
        public async Task<IActionResult> CreateOrderDetail([FromBody] OrderDetailsCreateVM request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<OrderDetailsCreateVM>.Error("Dữ liệu không hợp lệ"));
            var detail = _mapper.Map<OrderDetailsCreateVM,OrderDetails>(request);
            await _orderDetailsService.CreateAsync(detail);
            return CreatedAtAction(nameof(GetOrderDetail), new { id = detail.id }, detail);
        }

        [HttpPut]
        [JwtAuthorize("Admin", "Staff")]
        public async Task<IActionResult> UpdateOrderDetail([FromBody] OrderDetailsUpdateVM request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<OrderDetailsUpdateVM>.Error("Dữ liệu không hợp lệ"));
            var detail = await _orderDetailsService.GetByIdAsync(request.Id);
            if (detail == null)
                return NotFound();
            _mapper.Map(request, detail);
            await _orderDetailsService.UpdateAsync(detail);
            return Ok(ApiResponse<OrderDetailsUpdateVM>.Ok(request, "Cập nhật chi tiết đơn hàng thành công"));
        }

        [HttpDelete("{id}")]
        [JwtAuthorize("Admin")]
        public async Task<IActionResult> DeleteOrderDetail(Guid id)
        {
            var detail = await _orderDetailsService.GetByIdAsync(id);
            if (detail == null)
                return NotFound();
            await _orderDetailsService.DeleteAsync(detail);
            return Ok(ApiResponse<OrderDetailsDto>.Ok(null, "Xóa chi tiết đơn hàng thành công"));
        }
    }
}