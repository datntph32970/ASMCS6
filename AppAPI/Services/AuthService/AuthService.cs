using AppAPI.Repositories.BaseRepository;
using AppAPI.Services.AuthService.ViewModels;
using AppAPI.Services.JwtService;
using AppAPI.Services.RolesService;
using AppAPI.Services.UsersService;
using AppDB.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace AppAPI.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IUsersService _usersService;
        private readonly IRolesService _rolesService;
        private readonly IJwtService _jwtService;

        public AuthService(
            IUsersService usersService,
            IRolesService rolesService,
            IJwtService jwtService)
        {
            _jwtService = jwtService;
            _usersService = usersService;
            _rolesService = rolesService;
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            try
            {
                var user = await _usersService.GetQueryable()
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Username == request.Username);

                if (user == null)
                {
                    return new AuthResponse
                    {
                        Success = false,
                        Message = "Invalid username or password"
                    };
                }

                if (!VerifyPassword(request.Password, user.Password))
                {
                    return new AuthResponse
                    {
                        Success = false,
                        Message = "Invalid username or password"
                    };
                }

                var token = _jwtService.GenerateToken(user);

                return new AuthResponse
                {
                    Success = true,
                    Message = "Login successful",
                    Token = token,
                    User = new UserInfo
                    {
                        Id = user.id,
                        Username = user.Username,
                        FullName = user.FullName,
                        Email = user.Email,
                        Phone = user.Phone,
                        Address = user.Address,
                        RoleId = user.RoleId,
                        RoleName = user.Role?.RoleName ?? ""
                    }
                };
            }
            catch (Exception ex)
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = $"Login failed: {ex.Message}"
                };
            }
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            try
            {
                // Check if username already exists
                var existingUser = await _usersService.GetQueryable()
                    .FirstOrDefaultAsync(u => u.Username == request.Username);

                if (existingUser != null)
                {
                    return new AuthResponse
                    {
                        Success = false,
                        Message = "Username already exists"
                    };
                }

                // Check if email already exists
                if (!string.IsNullOrEmpty(request.Email))
                {
                    var existingEmail = await _usersService.GetQueryable()
                        .FirstOrDefaultAsync(u => u.Email == request.Email);

                    if (existingEmail != null)
                    {
                        return new AuthResponse
                        {
                            Success = false,
                            Message = "Email already exists"
                        };
                    }
                }

                // Verify role exists
                var role = await _rolesService.GetByIdAsync(request.RoleId);
                if (role == null)
                {
                    return new AuthResponse
                    {
                        Success = false,
                        Message = "Invalid role"
                    };
                }

                var hashedPassword = HashPassword(request.Password);

                var newUser = new Users
                {
                    Username = request.Username,
                    Password = hashedPassword,
                    FullName = request.FullName,
                    Email = request.Email,
                    Phone = request.Phone,
                    Address = request.Address,
                    RoleId = request.RoleId
                };

                await _usersService.CreateAsync(newUser);

                var token = _jwtService.GenerateToken(newUser);

                return new AuthResponse
                {
                    Success = true,
                    Message = "Registration successful",
                    Token = token,
                    User = new UserInfo
                    {
                        Id = newUser.id,
                        Username = newUser.Username,
                        FullName = newUser.FullName,
                        Email = newUser.Email,
                        Phone = newUser.Phone,
                        Address = newUser.Address,
                        RoleId = newUser.RoleId,
                        RoleName = role.RoleName
                    }
                };
            }
            catch (Exception ex)
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = $"Registration failed: {ex.Message}"
                };
            }
        }

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            var user = await _usersService.GetQueryable()
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
                return false;

            return VerifyPassword(password, user.Password);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            var hashedInput = HashPassword(password);
            return hashedInput == hashedPassword;
        }
    }
}