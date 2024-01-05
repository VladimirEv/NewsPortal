namespace NewsPortal.Policies.Requirements.Handlers
{
    public class AdminRequirementHandler : AuthorizationHandler<AdminRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
        {
            var roleNameClaim = context.User.FindFirst(c => c.Type == AuthorizationClaims.Role)?.Value;
            if (roleNameClaim == Constants.Admin)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
