using Microsoft.AspNetCore.Identity;

namespace NewsPortal.Domain.Entities
{
    public class UserRole : IdentityUserRole<Guid>, ITimeStamp
    {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
