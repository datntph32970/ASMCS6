# Hệ thống Quản lý Đơn hàng - Frontend

## Mô tả

Đây là ứng dụng frontend được xây dựng bằng Blazor WebAssembly để quản lý hệ thống đơn hàng. Ứng dụng sử dụng API từ backend AppAPI để thực hiện các chức năng.

## Tính năng

### Authentication

- ✅ Đăng nhập với username/password
- ✅ Đăng ký tài khoản mới
- ✅ JWT Token authentication
- ✅ Auto-logout khi token hết hạn
- ✅ Role-based authorization

### Quản lý hệ thống

- ✅ Dashboard với các module chính
- ✅ Navigation menu theo role
- ✅ Responsive design với Bootstrap

## Cấu trúc dự án

```
AppView/
├── Pages/                 # Các trang của ứng dụng
│   ├── Home.razor        # Trang chủ
│   ├── Login.razor       # Trang đăng nhập
│   ├── Register.razor    # Trang đăng ký
│   └── NotFound.razor    # Trang 404
├── Services/             # Các service
│   ├── IApiService.cs    # Interface cho API service
│   ├── ApiService.cs     # Implementation của API service
│   ├── AuthenticationStateProvider.cs # Custom auth provider
│   └── JwtAuthorizationMessageHandler.cs # JWT handler
├── Layout/               # Layout components
│   ├── MainLayout.razor  # Layout chính
│   └── NavMenu.razor     # Navigation menu
└── Shared/               # Shared components
    └── RedirectToLogin.razor # Component redirect
```

## Cài đặt và chạy

### Yêu cầu

- .NET 8.0 SDK
- Backend API chạy trên `http://localhost:7294`

### Chạy ứng dụng

1. Đảm bảo backend API đang chạy:

```bash
cd AppAPI
dotnet run
```

2. Chạy frontend:

```bash
cd AppView
dotnet run
```

3. Mở trình duyệt và truy cập: `https://localhost:7295`

## API Endpoints

### Authentication

- `POST /api/Auth/login` - Đăng nhập
- `POST /api/Auth/register` - Đăng ký
- `GET /api/Auth/validate` - Xác thực user

### Users

- `GET /api/Users` - Lấy danh sách users
- `GET /api/Users/{id}` - Lấy thông tin user
- `POST /api/Users` - Tạo user mới
- `PUT /api/Users` - Cập nhật user
- `DELETE /api/Users/{id}` - Xóa user

### Products

- `GET /api/Products` - Lấy danh sách sản phẩm
- `GET /api/Products/{id}` - Lấy thông tin sản phẩm
- `POST /api/Products` - Tạo sản phẩm mới
- `PUT /api/Products` - Cập nhật sản phẩm
- `DELETE /api/Products/{id}` - Xóa sản phẩm

### Categories

- `GET /api/Categories` - Lấy danh sách danh mục
- `GET /api/Categories/{id}` - Lấy thông tin danh mục
- `POST /api/Categories` - Tạo danh mục mới
- `PUT /api/Categories` - Cập nhật danh mục
- `DELETE /api/Categories/{id}` - Xóa danh mục

### Orders

- `GET /api/Orders` - Lấy danh sách đơn hàng
- `GET /api/Orders/{id}` - Lấy thông tin đơn hàng
- `POST /api/Orders` - Tạo đơn hàng mới
- `PUT /api/Orders` - Cập nhật đơn hàng
- `DELETE /api/Orders/{id}` - Xóa đơn hàng

## Cấu hình

### Base URL

Mặc định API base URL được cấu hình là `http://localhost:7294/api`.
Có thể thay đổi trong file `AppView/Services/ApiService.cs`.

### JWT Token

Token được lưu trong localStorage với key `auth_token`.
Token sẽ được tự động thêm vào header Authorization cho tất cả API calls.

## Tính năng bảo mật

- JWT Token authentication
- Role-based authorization
- Auto token refresh (có thể implement thêm)
- Secure token storage trong localStorage
- CORS configuration (cần cấu hình ở backend)

## Phát triển thêm

### Thêm trang mới

1. Tạo file `.razor` trong thư mục `Pages/`
2. Thêm route với `@page "/route-name"`
3. Thêm link trong `NavMenu.razor` nếu cần

### Thêm API call mới

1. Thêm method vào `IApiService.cs`
2. Implement trong `ApiService.cs`
3. Sử dụng trong component

### Custom styling

- Sử dụng Bootstrap classes
- Thêm CSS trong `wwwroot/css/app.css`
- Hoặc tạo file CSS riêng cho component

## Troubleshooting

### Lỗi CORS

Đảm bảo backend API đã cấu hình CORS cho domain của frontend.

### Lỗi kết nối API

Kiểm tra:

- Backend API có đang chạy không
- Base URL có đúng không
- Port có đúng không

### Lỗi authentication

- Kiểm tra JWT token có hợp lệ không
- Token có bị expired không
- API endpoint có đúng không

## Contributing

1. Fork repository
2. Tạo feature branch
3. Commit changes
4. Push to branch
5. Tạo Pull Request

## License

MIT License
