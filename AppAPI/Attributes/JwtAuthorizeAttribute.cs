using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace AppAPI.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class JwtAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] _roles;

        public JwtAuthorizeAttribute(params string[] roles)
        {
            _roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity?.IsAuthenticated ?? true)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (_roles != null && _roles.Length > 0)
            {
                var userRole = user.FindFirst("RoleName")?.Value;
                if (string.IsNullOrEmpty(userRole) || !_roles.Contains(userRole))
                {
                    context.Result = new ForbidResult();
                    return;
                }
            }
        }
    }
}