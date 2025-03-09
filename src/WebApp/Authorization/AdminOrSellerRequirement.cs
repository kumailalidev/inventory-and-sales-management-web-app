using Microsoft.AspNetCore.Authorization;

namespace WebApp.Authorization
{
    public class AdminOrSellerRequirement : IAuthorizationRequirement
    {
        // Custom requirement doesn't need any properties for this scenario
    }

}
