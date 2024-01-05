using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsPortal.Domain.Entities;

namespace NewsPortal.Persistance.Configuration
{
    public class UserLoginEntityConfiguration : IEntityTypeConfiguration<UserLogin>
    {       
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.ToTable("UserLogins");
        }
    }
}
