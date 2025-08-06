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
        // Lưu thông tin người dùng vào session
        HttpContext.Session.SetString("Token", result.Data.Token);
        HttpContext.Session.SetString("UserName", result.Data.User.FullName);
        HttpContext.Session.SetString("UserRole", result.Data.User.RoleName);
        HttpContext.Session.SetString("UserId", result.Data.User.Id.ToString());

        TempData["ToastMessage"] = "Đăng nhập thành công!";
        TempData["ToastType"] = "success";
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

    [HttpGet]
    public IActionResult Logout()
    {
        // Xóa thông tin session
        HttpContext.Session.Clear();
        TempData["ToastMessage"] = "Đã đăng xuất thành công!";
        TempData["ToastType"] = "info";
        return RedirectToAction("Login");
    }
}