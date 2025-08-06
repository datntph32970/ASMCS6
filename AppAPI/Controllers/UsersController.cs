using AppAPI.Attributes;
using AppAPI.Services.UsersService;
using AppAPI.Services.UsersService.ViewModels;
using AppDB.Models.Entity;
using AutoMapper;
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
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var result = await _usersService.GetDto(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [JwtAuthorize("Admin")]
        public async Task<IActionResult> CreateUser([FromBody] UsersCreateVM request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var entity = _mapper.Map<UsersCreateVM, Users>(request);
            await _usersService.CreateAsync(entity);
            return CreatedAtAction(nameof(GetUser), new { id = entity.id }, entity);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UsersUpdateVM request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var existingUser = await _usersService.GetByIdAsync(request.Id);
            if (existingUser == null)
                return NotFound();
            // Map the request to the existing user entity
            var entity = _mapper.Map<UsersUpdateVM, Users>(request, existingUser);
            await _usersService.UpdateAsync(entity);
            return Ok(entity);
        }

        [HttpDelete("{id}")]
        [JwtAuthorize("Admin")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var existingUser = await _usersService.GetByIdAsync(id);
            if (existingUser == null)
                return NotFound();
             await _usersService.DeleteAsync(existingUser);
            return NoContent();
        }
    }
}