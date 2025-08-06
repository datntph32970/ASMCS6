using AppAPI.Services.AuthService;
using AppAPI.Services.AuthService.ViewModels;
using AppDB.Models.DtoAndViewModels.AuthService.ViewModels;
using Microsoft.AspNetCore.Mvc;
using AuthResponse = AppAPI.Services.AuthService.ViewModels.AuthResponse;
using LoginRequest = AppAPI.Services.AuthService.ViewModels.LoginRequest;

namespace AppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<AuthResponse>>> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.LoginAsync(request);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.RegisterAsync(request);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("validate")]
        public async Task<ActionResult<bool>> ValidateUser([FromQuery] string username, [FromQuery] string password)
        {
            var isValid = await _authService.ValidateUserAsync(username, password);
            return Ok(isValid);
        }
    }
}