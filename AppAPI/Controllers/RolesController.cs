using AppAPI.Attributes;
using AppAPI.Services.RolesService;
using AppAPI.Services.RolesService.ViewModels;
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
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole(Guid id)
        {
            var result = await _rolesService.GetDto(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [JwtAuthorize("Admin")]
        public async Task<IActionResult> CreateRole([FromBody] RolesCreateVM request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newRole = new AppDB.Models.Roles
            {
                RoleName = request.RoleName
            };

            await _rolesService.CreateAsync(newRole);
            return CreatedAtAction(nameof(GetRole), new { id = newRole.id }, newRole);
        }

        [HttpPut("{id}")]
        [JwtAuthorize("Admin")]
        public async Task<IActionResult> UpdateRole(Guid id, [FromBody] RolesUpdateVM request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingRole = await _rolesService.GetByIdAsync(id);
            if (existingRole == null)
                return NotFound();

            existingRole.RoleName = request.RoleName;
            await _rolesService.UpdateAsync(existingRole);

            return Ok(existingRole);
        }

        [HttpDelete("{id}")]
        [JwtAuthorize("Admin")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            var role = await _rolesService.GetByIdAsync(id);
            if (role == null)
                return NotFound();

            await _rolesService.DeleteAsync(role);
            return NoContent();
        }
    }
}