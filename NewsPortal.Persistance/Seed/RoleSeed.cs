namespace NewsPortal.Persistance.Seed
{
    public static class RoleSeed
    {
        public static void AddRoles(ModelBuilder builder)
        {
            builder.Entity<Role>().HasData(GetSeedRoles());
        }

        private static IReadOnlyCollection<Role> GetSeedRoles()
        {
            return new List<Role>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "User",
                    RoleWeight = Roles.User,
                    NormalizedName = "USER".Normalize(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    IsDefault = true,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Admin",
                    RoleWeight = Roles.Admin,
                    NormalizedName = "ADMIN".Normalize(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    IsDefault = false,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                }

            };
        }
    }
}
