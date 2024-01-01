namespace NewsPortal.BusinessLogic.Common.Injector
{
    public static class LogicInjector
    {
        public static void Register (IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IJwtService, JwtService>();
        }
    }
}
