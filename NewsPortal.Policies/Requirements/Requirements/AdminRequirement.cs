
namespace NewsPortal.Policies.Requirements.Requirements
{
    public class AdminRequirement : IAuthorizationRequirement
    {
        public AdminRequirement()
        {
            RoleName = Constants.Admin;
        }

        public string RoleName { get; }
    }
}
