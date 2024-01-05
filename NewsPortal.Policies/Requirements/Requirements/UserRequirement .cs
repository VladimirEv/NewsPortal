
namespace NewsPortal.Policies.Requirements.Requirements
{
    public class UserRequirement : IAuthorizationRequirement
    {
        public UserRequirement()
        {
            RoleName = Constants.User;
        }

        public string RoleName { get; }
    }
}
