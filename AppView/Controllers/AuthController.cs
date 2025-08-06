using AppDB.Models.DtoAndViewModels.AuthService.ViewModels;
using AppView.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppView.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IJwtService _jwtService;
        private readonly IRolesService _rolesService;

        public AuthController(IAuthService authService, IJwtService jwtService, IRolesService rolesService)
        {
            _authService = authService;
            _jwtService = jwtService;
            _rolesService = rolesService;
        }

        // GET: Auth/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Auth/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            if (ModelState.IsValid)
            {
                var response = await _authService.LoginAsync(model);

                if (response.Success)
                {
                    // Store token using JwtService
                    _jwtService.SetToken(response.Data.Token);
                    TempData["SuccessMessage"] = "Login successful!";
                    return RedirectToAction("Index", "Home");
                }

                TempData["ErrorMessage"] = response.Message;
            }

            return View(model);
        }

        // GET: Auth/Register
        public async Task<IActionResult> Register()
        {
            // Load roles for dropdown
            var rolesResponse = await _rolesService.GetAllAsync();
            ViewBag.Roles = rolesResponse.Success ? rolesResponse.Data : new List<AppDB.Models.DtoAndViewModels.RolesService.Dto.RolesDto>();

            return View();
        }

        // POST: Auth/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            if (ModelState.IsValid)
            {
                var response = await _authService.RegisterAsync(model);

                if (response.Success)
                {
                    TempData["SuccessMessage"] = "Registration successful! Please login.";
                    return RedirectToAction(nameof(Login));
                }

                TempData["ErrorMessage"] = response.Message;
            }

            // Reload roles for dropdown
            var rolesResponse = await _rolesService.GetAllAsync();
            ViewBag.Roles = rolesResponse.Success ? rolesResponse.Data : new List<AppDB.Models.DtoAndViewModels.RolesService.Dto.RolesDto>();

            return View(model);
        }

        // POST: Auth/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // Clear token using JwtService (client-side logout)
            _jwtService.ClearToken();

            // Gọi service để log (optional)
            await _authService.LogoutAsync();

            TempData["SuccessMessage"] = "Logout successful!";
            return RedirectToAction("Index", "Home");
        }
    }
}