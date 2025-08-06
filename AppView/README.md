# AppView - MVC Application

Ứng dụng MVC để gọi và sử dụng các API từ AppAPI.

## Cấu trúc dự án

### 1. Models

- `ApiResponse<T>`: Model để xử lý response từ API
- `ApiResponse`: Model cho response không có data

### 2. Services

- `IBaseApiService` & `BaseApiService`: Service cơ bản để gọi API
- `IProductsService` & `ProductsService`: Service cho Products
- `ICategoriesService` & `CategoriesService`: Service cho Categories
- `IAuthService` & `AuthService`: Service cho Authentication
- `IJwtService` & `JwtService`: Service quản lý JWT token

### 3. Controllers

- `ProductsController`: Controller quản lý Products
- `CategoriesController`: Controller quản lý Categories
- `AuthController`: Controller xử lý đăng nhập/đăng ký
- `HomeController`: Controller trang chủ

## Cách sử dụng

### 1. Cấu hình API

Trong `appsettings.json`:

```json
{
  "ApiSettings": {
    "BaseUrl": "https://localhost:7001"
  }
}
```

### 2. Sử dụng trong Controller

```csharp
public class ProductsController : Controller
{
    private readonly IProductsService _productsService;

    public ProductsController(IProductsService productsService)
    {
        _productsService = productsService;
    }

    public async Task<IActionResult> Index()
    {
        var response = await _productsService.GetAllAsync();

        if (response.Success)
        {
            return View(response.Data);
        }

        TempData["ErrorMessage"] = response.Message;
        return View(new List<ProductsDto>());
    }
}
```

### 3. Authentication

- Sử dụng `IAuthService` để đăng nhập/đăng ký
- JWT token được tự động thêm vào header của các request API
- Sử dụng `IJwtService` để quản lý token
- Logout chỉ clear token ở client, không cần gọi API

### 4. File Upload

Để upload file (ví dụ: tạo product với hình ảnh):

```csharp
public async Task<IActionResult> Create(ProductsCreateVM model)
{
    if (ModelState.IsValid)
    {
        var response = await _productsService.CreateAsync(model);
        // model.Image sẽ được upload cùng với các data khác
    }
}
```

### 5. Categories Management

Quản lý categories với đầy đủ CRUD operations:

```csharp
public class CategoriesController : Controller
{
    private readonly ICategoriesService _categoriesService;

    public CategoriesController(ICategoriesService categoriesService)
    {
        _categoriesService = categoriesService;
    }

    public async Task<IActionResult> Index()
    {
        var response = await _categoriesService.GetAllAsync();
        return View(response.Data);
    }
}
```

### 6. Authentication System

Hệ thống đăng nhập/đăng ký hoàn chỉnh:

```csharp
public class AuthController : Controller
{
    private readonly IAuthService _authService;
    private readonly IJwtService _jwtService;

    public async Task<IActionResult> Login(LoginRequest model)
    {
        var response = await _authService.LoginAsync(model);
        if (response.Success)
        {
            _jwtService.SetToken(response.Data);
            return RedirectToAction("Index", "Home");
        }
        return View(model);
    }
}
```

## Các tính năng

1. **HTTP Client Management**: Tự động quản lý HTTP client với base URL
2. **JWT Authentication**: Tự động thêm JWT token vào header
3. **Error Handling**: Xử lý lỗi và trả về message phù hợp
4. **File Upload**: Hỗ trợ upload file qua multipart form data
5. **Session Management**: Quản lý session cho JWT token

## Dependencies

- `Microsoft.Extensions.Http`: HTTP client factory
- `Newtonsoft.Json`: JSON serialization
- `AppDB`: Reference đến project chứa DTO và ViewModels

## Chạy ứng dụng

1. Đảm bảo AppAPI đang chạy trên port 7001
2. Chạy AppView project
3. Truy cập các controller để test các chức năng
