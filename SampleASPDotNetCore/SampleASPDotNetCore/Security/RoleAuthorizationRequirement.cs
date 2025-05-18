using Microsoft.AspNetCore.Authorization;

namespace SampleASPDotNetCore.Security
{
    /// <summary>
    /// Rolebased Authorization requirement. This will be used to check if the user is having Admin role or not.
    /// </summary>
    public class RoleAuthorizationRequirement : IAuthorizationRequirement
    {
    }
    public class RoleAuthorizationRequirementHandler : AuthorizationHandler<RoleAuthorizationRequirement>
    {
        private IHttpContextAccessor _httpContextAccessor;
        public RoleAuthorizationRequirementHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleAuthorizationRequirement requirement)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (requirement == null) throw new ArgumentNullException(nameof(requirement));
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null) return;
            bool isAuthenticated = context.User.Identities.Any(x => x.IsAuthenticated);
           
            var roleClaims = context?.User.Claims.FirstOrDefault(x => x.Type == "custom_role")?.Value.ToString();
            if (isAuthenticated && !string.IsNullOrEmpty(roleClaims) && roleClaims == "Admin")
            {
                context?.Succeed(requirement);
            }
          
        }
    }
}
