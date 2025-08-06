using AppAPI.Attributes;
using AppAPI.Services.MapperService;
using AppAPI.Services.UsersService;
using AppDB.Models.DtoAndViewModels.AuthService.ViewModels;
using AppDB.Models.DtoAndViewModels.BaseServices.Common;
using AppDB.Models.DtoAndViewModels.UsersService.Dto;
using AppDB.Models.DtoAndViewModels.UsersService.ViewModels;
using AppDB.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public UsersController(IUsersService usersService, IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }

        [HttpGet]
        [JwtAuthorize("Admin")]
        public async Task<IActionResult> GetUsers([FromQuery] UsersSearch search)
        {
            var result = await _usersService.GetData(search);
            return Ok(ApiResponse<PagedList<UsersDto>>.Ok(result,"Lấy danh sách thành công"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var result = await _usersService.GetDto(id);
            if (result == null)
                return NotFound();
            return Ok(ApiResponse<UsersDto>.Ok(result, "Lấy thông tin người dùng thành công"));
        }

        [HttpPost]
        [JwtAuthorize("Admin")]
        public async Task<IActionResult> CreateUser([FromBody] UsersCreateVM request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<UsersCreateVM>.Error("Dữ liệu không hợp lệ"));
            var entity = _mapper.Map<UsersCreateVM, Users>(request);
            await _usersService.CreateAsync(entity);
            return CreatedAtAction(nameof(GetUser), new { id = entity.id }, entity);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UsersUpdateVM request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<UsersUpdateVM>.Error("Dữ liệu không hợp lệ"));
            var existingUser = await _usersService.GetByIdAsync(request.Id);
            if (existingUser == null)
                return NotFound();
            // Map the request to the existing user entity
            var entity = _mapper.Map<UsersUpdateVM, Users>(request, existingUser);
            await _usersService.UpdateAsync(entity);
            return Ok(ApiResponse<UsersUpdateVM>.Ok(request, "Cập nhật người dùng thành công"));
        }

        [HttpDelete("{id}")]
        [JwtAuthorize("Admin")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var existingUser = await _usersService.GetByIdAsync(id);
            if (existingUser == null)
                return NotFound();
             await _usersService.DeleteAsync(existingUser);
            return Ok(ApiResponse<UsersDto>.Ok(null, "Xóa người dùng thành công"));
        }
    }
}