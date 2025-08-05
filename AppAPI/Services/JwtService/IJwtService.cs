using AppDB.Models;

namespace AppAPI.Services.JwtService
{
    public interface IJwtService
    {
        string GenerateToken(Users user);
        bool ValidateToken(string token);
        string? GetUserIdFromToken(string token);
        string? GetUsernameFromToken(string token);
    }
}