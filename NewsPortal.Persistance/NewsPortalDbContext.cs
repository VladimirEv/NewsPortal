using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using NewsPortal.Domain.Entities;

namespace NewsPortal.Persistance
{
    public class NewsPortalDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public NewsPortalDbContext(DbContextOptions options) : base(options)
        {
            Database.Migrate(); //cоздаём БД, если БД нет
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            RoleSeed.AddRoles(builder);

            ///<summary>
            ///Этот код является частью Entity Framework Core и используется для автоматической настройки 
            ///маппинга сущностей базы данных из сборки(assembly) приложения.
            ///1. Assembly.GetExecutingAssembly(): Этот метод возвращает сборку (assembly), в которой выполняется текущий код. 
            ///В данном случае, это будет сборка, в которой находится этот код.
            ///2. builder: Это объект ModelBuilder, предоставляемый Entity Framework Core для настройки модели базы данных. 
            ///Обычно он используется в OnModelCreating методе, который может быть определен в классе, унаследованном от DbContext.
            ///3. ApplyConfigurationsFromAssembly: Это метод ModelBuilder, который сканирует указанную сборку (в данном случае, текущую сборку) 
            ///в поисках всех классов, реализующих интерфейс IEntityTypeConfiguration<T>, и автоматически применяет их конфигурации 
            ///к соответствующим сущностям в модели базы данных.
            ///</summary>
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }

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
