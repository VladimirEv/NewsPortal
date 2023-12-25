﻿using Microsoft.AspNetCore.Identity;

namespace NewsPortal.Domain.Entities
{
    public class UserClaim : IdentityUserClaim<Guid>, ITimeStamp
    {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
