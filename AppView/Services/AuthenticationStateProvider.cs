using AppDB.Models.DtoAndViewModels.AuthService.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace AppView.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IApiService _apiService;

        public CustomAuthenticationStateProvider(IApiService apiService)
        {
            _apiService = apiService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var isAuthenticated = await _apiService.IsAuthenticatedAsync();

            if (!isAuthenticated)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            // Get token and user information
            var token = await _apiService.GetTokenAsync();
            var userInfo = await _apiService.GetUserInfoAsync();

            if (string.IsNullOrEmpty(token) || userInfo == null)
            {
                // Clear invalid data
                await _apiService.ClearTokenAsync();
                await _apiService.ClearUserInfoAsync();
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            try
            {
                // Create claims from user information
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userInfo.Id.ToString()),
                    new Claim(ClaimTypes.Name, userInfo.Username),
                    new Claim(ClaimTypes.GivenName, userInfo.FullName),
                    new Claim(ClaimTypes.Email, userInfo.Email ?? ""),
                    new Claim(ClaimTypes.Role, userInfo.RoleName),
                    new Claim("UserId", userInfo.Id.ToString()),
                    new Claim("RoleId", userInfo.RoleId.ToString()),
                    new Claim("Phone", userInfo.Phone ?? ""),
                    new Claim("Address", userInfo.Address ?? ""),
                    new Claim("token", token)
                };

                var identity = new ClaimsIdentity(claims, "jwt");
                var user = new ClaimsPrincipal(identity);

                return new AuthenticationState(user);
            }
            catch
            {
                // If token is invalid, clear it and return unauthenticated
                await _apiService.ClearTokenAsync();
                await _apiService.ClearUserInfoAsync();
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }

        public void NotifyAuthenticationStateChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
