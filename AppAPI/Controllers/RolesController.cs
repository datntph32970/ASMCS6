using AppAPI.Attributes;
using AppAPI.Services.RolesService;
using AppDB.Models.DtoAndViewModels.AuthService.ViewModels;
using AppDB.Models.DtoAndViewModels.BaseServices.Common;
using AppDB.Models.DtoAndViewModels.RolesService.Dto;
using AppDB.Models.DtoAndViewModels.RolesService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _rolesService;

        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles([FromQuery] RolesSearch search)
        {
            var result = await _rolesService.GetData(search);
            return Ok(ApiResponse<PagedList<RolesDto>>.Ok(result, "Lấy danh sách vai trò thành công"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole(Guid id)
        {
            var result = await _rolesService.GetDto(id);
            if (result == null)
                return NotFound();

            return Ok(ApiResponse<RolesDto>.Ok(result, "Lấy thông tin vai trò thành công"));
        }

        [HttpPost]
        [JwtAuthorize("Admin")]
        public async Task<IActionResult> CreateRole([FromBody] RolesCreateVM request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<RolesCreateVM>.Error("Dữ liệu không hợp lệ"));

            var newRole = new AppDB.Models.Entity.Roles
            {
                RoleName = request.RoleName
            };

            await _rolesService.CreateAsync(newRole);
            return CreatedAtAction(nameof(GetRole), new { id = newRole.id }, newRole);
        }

        [HttpPut]
        [JwtAuthorize("Admin")]
        public async Task<IActionResult> UpdateRole( [FromBody] RolesUpdateVM request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<RolesUpdateVM>.Error("Dữ liệu không hợp lệ"));

            var existingRole = await _rolesService.GetByIdAsync(request.Id);
            if (existingRole == null)
                return NotFound();

            existingRole.RoleName = request.RoleName;
            await _rolesService.UpdateAsync(existingRole);

            return Ok(ApiResponse<RolesUpdateVM>.Ok(request, "Cập nhật vai trò thành công"));
        }

        [HttpDelete("{id}")]
        [JwtAuthorize("Admin")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            var role = await _rolesService.GetByIdAsync(id);
            if (role == null)
                return NotFound();

            await _rolesService.DeleteAsync(role);
            return Ok(ApiResponse<string>.Ok("Xóa vai trò thành công"));
        }
    }
}