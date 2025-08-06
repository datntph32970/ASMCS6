using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AppDB.Models.DtoAndViewModels.AuthService.ViewModels;

public class AccountController : Controller
{
    private readonly IAuthService _authService;
    public AccountController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest model)
    {
        if (!ModelState.IsValid)
            return View(model);
        var result = await _authService.LoginAsync(model);
        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View(model);
        }
        // Lưu token vào session/cookie nếu cần
        // HttpContext.Session.SetString("Token", result.Data.Token);
        TempData["ToastMessage"] = "Đăng nhập thành công!";
        TempData["ToastType"] = "success"; // hoặc "danger", "info", "warning"
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequest model)
    {
        if (!ModelState.IsValid)
            return View(model);

        try
        {
            var result = await _authService.RegisterAsync(model);
            if (!result.Success)
            {
                TempData["ToastMessage"] = result.Message;
                TempData["ToastType"] = "danger";
                return View(model);
            }
            TempData["ToastMessage"] = "Đăng ký thành công! Vui lòng đăng nhập.";
            TempData["ToastType"] = "success";
            return RedirectToAction("Login");
        }
        catch (Exception ex)
        {
            TempData["ToastMessage"] = "Đăng ký thất bại: " + ex.Message;
            TempData["ToastType"] = "danger";
            return View(model);
        }
    }
}