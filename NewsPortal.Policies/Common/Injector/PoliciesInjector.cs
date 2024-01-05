using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace NewsPortal.Policies.Common.Injector
{
    public static class PoliciesInjector
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthorizationPolicies.AdminPolicy, p => p.AddRequirements(new AdminRequirement()));
                options.AddPolicy(AuthorizationPolicies.UserPolicy, p => p.AddRequirements(new UserRequirement()));
            });

            services.AddSingleton<IAuthorizationHandler, AdminRequirementHandler>();
            services.AddSingleton<IAuthorizationHandler, UserRequirementHandler>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = configuration[ConfigurationConstants.JwtValidAudience],
                        ValidIssuer = configuration[ConfigurationConstants.JwtValidIssuer],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration[ConfigurationConstants.JwtSecret] ?? string.Empty)),
                        ClockSkew = TimeSpan.Zero
                    };
                });
        }
    }
}
