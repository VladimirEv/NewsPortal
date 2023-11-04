using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using NewsPortal.Domain.Entities;

namespace NewsPortal.Persistance
{
    public class NewsPortalDbContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public NewsPortalDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated(); //cоздаём БД, если БД нет
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }

    //разобраться!!!
    public class NewsPortalDbContextFactory : IDesignTimeDbContextFactory<NewsPortalDbContext>
    {
        public NewsPortalDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<NewsPortalDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=auth;Username=postgres;Password=1111");

            return new NewsPortalDbContext(optionsBuilder.Options);
        }
    }
}
