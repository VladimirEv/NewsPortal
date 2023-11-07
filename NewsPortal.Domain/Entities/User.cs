using Microsoft.AspNetCore.Identity;
using NewsPortal.Domain.Interfaces.Entities;

namespace NewsPortal.Domain.Entities
{
    public class User : IdentityUser <int>, ITimeStamp
    {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public ICollection<UserRole>? UserRoles { get; set; }
    }
}
