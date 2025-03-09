using Microsoft.AspNetCore.Authorization;

namespace WebApp.Authorization
{
    // Custom authorization handler that checks if the user has 'Admin' or 'Manager' claims
    public class AdminOrManagerHandler : AuthorizationHandler<AdminOrManagerRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminOrManagerRequirement requirement)
        {
            // Check if the user has either 'Admin' or 'Manager' claim
            var result = context.User.HasClaim(c => c.Type == "Position" && (c.Value == "Admin" || c.Value == "Manager"));

            if (result)
            {
                context.Succeed(requirement); // Succeed if the user meets the requirement
            }

            return Task.CompletedTask;
        }
    }
}
