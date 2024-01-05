using Microsoft.AspNetCore.Identity;

namespace NewsPortal.Domain.Entities
{
    public class Role : IdentityRole<Guid>, ITimeStamp
    {
        public bool IsDefault { get; set; }
        public Roles RoleWeight { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
