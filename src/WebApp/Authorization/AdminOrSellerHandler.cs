using Microsoft.AspNetCore.Authorization;

namespace WebApp.Authorization
{
    // Custom authorization handler that checks if the user has 'Admin' or 'Seller' claims
    public class AdminOrSellerHandler : AuthorizationHandler<AdminOrSellerRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminOrSellerRequirement requirement)
        {
            // Check if the user has either 'Admin' or 'Manager' claim
            var result = context.User.HasClaim(c => c.Type == "Position" && (c.Value == "Admin" || c.Value == "Seller"));

            if (result)
            {
                context.Succeed(requirement); // Succeed if the user meets the requirement
            }

            return Task.CompletedTask;
        }
    }
}
