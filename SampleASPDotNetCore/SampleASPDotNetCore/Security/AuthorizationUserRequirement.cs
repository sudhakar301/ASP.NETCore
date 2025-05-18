using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;

namespace SampleASPDotNetCore.Security
{
    /// <summary>
    /// This class will be used for AUthorization purpose. It will be used to check if the user is authenticated or not.
    /// Claims will be read here and based on that it will be decided if the user is authenticated or not.
    /// </summary>
    public class AuthorizationUserRequirement : IAuthorizationRequirement
    {
    }
    public class AuthorizationUserRequirementHandler : AuthorizationHandler<AuthorizationUserRequirement>
    {
        private IHttpContextAccessor _httpContextAccessor;

        public AuthorizationUserRequirementHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizationUserRequirement requirement)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (requirement == null) throw new ArgumentNullException(nameof(requirement));

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null) return;
           

            bool isAuthenticated = context.User.Identities.Any(x => x.IsAuthenticated);
            var userID = httpContext.User?.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;


            if (isAuthenticated)
            {
                context.Succeed(requirement);
            }
        }
    }
}
