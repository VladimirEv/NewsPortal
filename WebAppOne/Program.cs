using NewsPortal.Persistance;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<NewsPortalDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddDbContext<NewsPortalDbContext>(opt =>
opt.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
