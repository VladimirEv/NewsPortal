using Microsoft.AspNetCore.Identity;

namespace NewsPortal.Domain.Entities
{
    public class UserLogin : IdentityUserLogin<Guid>, ITimeStamp
    {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
