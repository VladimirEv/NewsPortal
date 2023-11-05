using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsPortal.Domain.Entities;

namespace NewsPortal.Persistance.Configuration
{
    ///<summary>
    ///IEntityTypeConfiguration<RoleClaim> - интерфейс используется для настройки сущностей
    ///в Entity Framework Core
    ///</summary>

    public class RoleClaimEntityConfiguration : IEntityTypeConfiguration<RoleClaim>
    {
        /// <summary> Работа метода Configure.
        ///Внутри этого метода выполняются настройки сущности RoleClaim.
        ///устанавливается имя таблицы, в которой будут храниться данные сущности RoleClaim
        ///Строка builder.ToTable("RoleClaims") указывает, что сущность RoleClaimEntity будет
        ///отображаться в таблице с именем "RoleClaims" в базе данных
        /// </summary>
        
        public void Configure(EntityTypeBuilder<RoleClaim> builder)
        {
            builder.ToTable("RoleClaims");
        }
    }
}
