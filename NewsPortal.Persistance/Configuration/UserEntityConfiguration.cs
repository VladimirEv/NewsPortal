using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsPortal.Domain.Entities;

namespace NewsPortal.Persistance.Configuration
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {       
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
        }
    }
}
