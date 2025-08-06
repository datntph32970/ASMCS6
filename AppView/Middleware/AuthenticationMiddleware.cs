using AppView.Services;
using Microsoft.AspNetCore.Http;

namespace AppView.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IJwtService jwtService)
        {
            // Add authentication status to ViewData for use in views
            if (context.Items.ContainsKey("IsAuthenticated"))
            {
                context.Items["IsAuthenticated"] = jwtService.IsAuthenticated();
            }
            else
            {
                context.Items.Add("IsAuthenticated", jwtService.IsAuthenticated());
            }

            await _next(context);
        }
    }
}