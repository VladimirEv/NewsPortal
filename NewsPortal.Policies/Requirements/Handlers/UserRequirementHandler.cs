namespace NewsPortal.Policies.Requirements.Handlers
{
    public class UserRequirementHandler : AuthorizationHandler<UserRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserRequirement requirement)
        {
            var roleNameClaim = context.User.FindFirst(c => c.Type == AuthorizationClaims.Role)?.Value;
            if (roleNameClaim == Constants.User)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
