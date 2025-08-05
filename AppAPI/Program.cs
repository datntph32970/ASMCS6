using AppAPI.Repositories.BaseRepository;
using AppAPI.Repositories.CategoriesRepository;
using AppAPI.Repositories.ComboDetailsRepository;
using AppAPI.Repositories.CombosRepository;
using AppAPI.Repositories.OrderDetailsRepository;
using AppAPI.Repositories.OrdersRepository;
using AppAPI.Repositories.ProductsRepository;
using AppAPI.Repositories.RolesRepository;
using AppAPI.Repositories.StatusRepository;
using AppAPI.Repositories.StatusOrdersRepository;
using AppAPI.Repositories.UsersRepository;
using AppAPI.Services.AuthService;
using AppAPI.Services.CategoriesService;
using AppAPI.Services.ComboDetailsService;
using AppAPI.Services.CombosService;
using AppAPI.Services.JwtService;
using AppAPI.Services.JwtService.Dto;
using AppAPI.Services.OrderDetailsService;
using AppAPI.Services.OrdersService;
using AppAPI.Services.ProductsService;
using AppAPI.Services.RolesService;
using AppAPI.Services.StatusService;
using AppAPI.Services.StatusOrdersService;
using AppAPI.Services.UsersService;
using AppDB;
using AppDB.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
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

// Add HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Connect to the database
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Repositories
builder.Services.AddScoped<IBaseRepository<Users>, BaseRepository<Users>>();
builder.Services.AddScoped<IBaseRepository<Roles>, BaseRepository<Roles>>();
builder.Services.AddScoped<IBaseRepository<Products>, BaseRepository<Products>>();
builder.Services.AddScoped<IBaseRepository<Categories>, BaseRepository<Categories>>();
builder.Services.AddScoped<IBaseRepository<Orders>, BaseRepository<Orders>>();
builder.Services.AddScoped<IBaseRepository<OrderDetails>, BaseRepository<OrderDetails>>();
builder.Services.AddScoped<IBaseRepository<Combos>, BaseRepository<Combos>>();
builder.Services.AddScoped<IBaseRepository<ComboDetails>, BaseRepository<ComboDetails>>();
builder.Services.AddScoped<IBaseRepository<Status>, BaseRepository<Status>>();
builder.Services.AddScoped<IBaseRepository<StatusOrders>, BaseRepository<StatusOrders>>();

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IRolesRepository, RolesRepository>();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<IOrderDetailsRepository, OrderDetailsRepository>();
builder.Services.AddScoped<ICombosRepository, CombosRepository>();
builder.Services.AddScoped<IComboDetailsRepository, ComboDetailsRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<IStatusOrdersRepository, StatusOrdersRepository>();

// Register Services
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IRolesService, RolesService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<IOrderDetailsService, OrderDetailsService>();
builder.Services.AddScoped<ICombosService, CombosService>();
builder.Services.AddScoped<IComboDetailsService, ComboDetailsService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<IStatusOrdersService, StatusOrdersService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add CORS
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
