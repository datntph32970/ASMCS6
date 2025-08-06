using Microsoft.AspNetCore.Http;

namespace AppView.Services
{
    public class JwtService : IJwtService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JwtService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? GetToken()
        {
            return _httpContextAccessor.HttpContext?.Session.GetString("JWTToken");
        }

        public void SetToken(string token)
        {
            _httpContextAccessor.HttpContext?.Session.SetString("JWTToken", token);
        }

        public void ClearToken()
        {
            _httpContextAccessor.HttpContext?.Session.Remove("JWTToken");
        }

        public bool IsAuthenticated()
        {
            var token = GetToken();
            return !string.IsNullOrEmpty(token);
        }
    }
}