using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.JSInterop;
using AppDB.Models.DtoAndViewModels.AuthService.ViewModels;

namespace AppView.Services
{
    public class AuthState
    {
        public event Action? OnAuthStateChanged;
        public void NotifyAuthStateChanged() => OnAuthStateChanged?.Invoke();
    }

    public class AuthService
    {
        private readonly IJSRuntime _js;
        private readonly HttpClient _http;
        public AuthService(IJSRuntime js, HttpClient http)
        {
            _js = js;
            _http = http;
        }

        public async Task<string?> GetTokenAsync()
        {
            return await _js.InvokeAsync<string>("localStorage.getItem", "token");
        }

        public async Task SetTokenAsync(string token)
        {
            await _js.InvokeVoidAsync("localStorage.setItem", "token", token);
        }

        public async Task RemoveTokenAsync()
        {
            await _js.InvokeVoidAsync("localStorage.removeItem", "token");
        }

        public async Task<UserInfo?> GetUserInfoAsync()
        {
            var token = await GetTokenAsync();
            if (string.IsNullOrEmpty(token)) return null;
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);
                var userInfo = new UserInfo
                {
                    Id = Guid.Parse(jwt.Claims.FirstOrDefault(c => c.Type == "id")?.Value ?? Guid.Empty.ToString()),
                    Username = jwt.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value ?? string.Empty,
                    FullName = jwt.Claims.FirstOrDefault(c => c.Type == "fullname")?.Value ?? string.Empty,
                    Email = jwt.Claims.FirstOrDefault(c => c.Type == "email")?.Value ?? string.Empty,
                    Phone = jwt.Claims.FirstOrDefault(c => c.Type == "phone")?.Value ?? string.Empty,
                    Address = jwt.Claims.FirstOrDefault(c => c.Type == "address")?.Value ?? string.Empty,
                    RoleId = Guid.TryParse(jwt.Claims.FirstOrDefault(c => c.Type == "roleid")?.Value, out var rid) ? rid : Guid.Empty,
                    RoleName = jwt.Claims.FirstOrDefault(c => c.Type == "rolename")?.Value ?? string.Empty
                };
                return userInfo;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> IsLoggedInAsync()
        {
            var token = await GetTokenAsync();
            if (string.IsNullOrEmpty(token)) return false;
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);
                return jwt.ValidTo > DateTime.UtcNow;
            }
            catch
            {
                return false;
            }
        }
    }
}