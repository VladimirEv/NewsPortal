using Microsoft.AspNetCore.Identity;

namespace NewsPortal.Domain.Entities
{
    public class RoleClaim : IdentityRoleClaim<Guid>, ITimeStamp
    {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
