using Microsoft.AspNetCore.Authorization;

namespace WebApp.Authorization
{
    public class AdminOrManagerRequirement : IAuthorizationRequirement
    {
        // Custom requirement doesn't need any properties for this scenario
    }

}
