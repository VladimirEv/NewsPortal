using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsPortal.Domain.Entities;

namespace NewsPortal.Persistance.Configuration
{
    public class UserRoleEntityConfiguration : IEntityTypeConfiguration<UserRole>
    {       
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRoles");
        }
    }
}
