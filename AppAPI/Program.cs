using AppAPI.Repositories.BaseRepository;
using AppAPI.Repositories.CategoriesRepository;
using AppAPI.Repositories.ComboDetailsRepository;
using AppAPI.Repositories.CombosRepository;
using AppAPI.Repositories.OrderDetailsRepository;
using AppAPI.Repositories.OrdersRepository;
using AppAPI.Repositories.ProductsRepository;
using AppAPI.Repositories.RolesRepository;
using AppAPI.Repositories.StatusOrdersRepository;
using AppAPI.Repositories.StatusRepository;
using AppAPI.Repositories.UsersRepository;
using AppAPI.Services.AuthService;
using AppAPI.Services.BaseServices;
using AppAPI.Services.CategoriesService;
using AppAPI.Services.ComboDetailsService;
using AppAPI.Services.CombosService;
using AppAPI.Services.JwtService;
using AppAPI.Services.JwtService.Dto;
using AppAPI.Services.MapperService;
using AppAPI.Services.OrderDetailsService;
using AppAPI.Services.OrdersService;
using AppAPI.Services.ProductsService;
using AppAPI.Services.RolesService;
using AppAPI.Services.StatusOrdersService;
using AppAPI.Services.StatusService;
using AppAPI.Services.UsersService;
using AppDB;
using AppDB.Models.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


// Configure JWT Settings
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings?.Issuer,
        ValidAudience = jwtSettings?.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings?.SecretKey ?? ""))
    };
});

// Configure Swagger with JWT support
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

    // Add JWT authentication to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
//cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});
// Add HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Connect to the database
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#region DI
builder.Services.AddScoped<IMapper, Mapper>();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
var repositoryTypes = typeof(IBaseRepository<>).Assembly.GetTypes()
                 .Where(x => !string.IsNullOrEmpty(x.Namespace) && x.Namespace.StartsWith("AppAPI") && x.Name.EndsWith("Repository"));
foreach (var intf in repositoryTypes.Where(t => t.IsInterface))
{
    var impl = repositoryTypes.FirstOrDefault(c => c.IsClass && intf.Name.Substring(1) == c.Name);
    if (impl != null) builder.Services.AddScoped(intf, impl);
}

builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
var serviceTypes = typeof(IBaseService<>).Assembly.GetTypes()
     .Where(x => !string.IsNullOrEmpty(x.Namespace) && x.Namespace.StartsWith("AppAPI") && x.Name.EndsWith("Service"));
foreach (var intf in serviceTypes.Where(t => t.IsInterface))
{
    var impl = serviceTypes.FirstOrDefault(c => c.IsClass && intf.Name.Substring(1) == c.Name);
    if (impl != null) builder.Services.AddScoped(intf, impl);
}
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();

// Add CORS

app.UseCors("AllowAllOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
