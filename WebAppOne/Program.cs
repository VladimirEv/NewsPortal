using Serilog;


var builder = WebApplication.CreateBuilder(args);

var host = builder.Host;
var services = builder.Services;
var configuration = builder.Configuration;

host.UseSerilog((context, loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(context.Configuration);
});
services.AddControllers();
services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition(ServiceCollectionConstants.SwaggerName, new OpenApiSecurityScheme
    {
        Name = ServiceCollectionConstants.SwaggerName,
        Type = SecuritySchemeType.Http,
        Scheme = ServiceCollectionConstants.SwaggerScheme,
        BearerFormat = ServiceCollectionConstants.SwaggerBearerFormat,
        In = ParameterLocation.Header,
        Description = ServiceCollectionConstants.SwaggerDescription
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = ServiceCollectionConstants.SwaggerId
                }
            },
            new string[] { }
        }
    });
});
services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<NewsPortalDbContext>()
    .AddDefaultTokenProviders();
services.AddDbContext<NewsPortalDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString(ConfigurationConstants.PostgresConnection));
    options.EnableSensitiveDataLogging();
});

services.AddFluentValidationAutoValidation();
services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(RegisterModelValidator)));
services.AddAutoMapper(Assembly.GetAssembly(typeof(MapperConfiguration)));

PoliciesInjector.Register(services, configuration);
InfrastructureInjector.Register(services, configuration);
LogicInjector.Register(services);
;

// Application level
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
