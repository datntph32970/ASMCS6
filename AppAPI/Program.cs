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
using AppAPI.Services.CategoriesService;
using AppAPI.Services.ComboDetailsService;
using AppAPI.Services.CombosService;
using AppAPI.Services.JwtService;
using AppAPI.Services.JwtService.Dto;
using AppAPI.Services.OrderDetailsService;
using AppAPI.Services.OrdersService;
using AppAPI.Services.ProductsService;
using AppAPI.Services.RolesService;
using AppAPI.Services.StatusOrdersService;
using AppAPI.Services.StatusService;
using AppAPI.Services.UsersService;
using AppDB;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using AutoMapper;
using AppDB.Models.Entity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Configure AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    // Auto-register all mappings using reflection
    var viewModelTypes = typeof(Program).Assembly.GetTypes()
        .Where(t => t.Name.EndsWith("CreateVM") || t.Name.EndsWith("UpdateVM"))
        .ToList();

    var dtoTypes = typeof(Program).Assembly.GetTypes()
        .Where(t => t.Name.EndsWith("Dto"))
        .ToList();

    var modelTypes = typeof(BaseEntity).Assembly.GetTypes()
        .Where(t => t.IsSubclassOf(typeof(BaseEntity)))
        .ToList();

    // Create mappings for ViewModels to Models
    foreach (var viewModelType in viewModelTypes)
    {
        var modelType = modelTypes.FirstOrDefault(m =>
            viewModelType.Name.Replace("CreateVM", "").Replace("UpdateVM", "") == m.Name);

        if (modelType != null)
        {
            cfg.CreateMap(viewModelType, modelType);
        }
    }

    // Create mappings for Models to DTOs
    foreach (var dtoType in dtoTypes)
    {
        var modelType = modelTypes.FirstOrDefault(m =>
            dtoType.Name.Replace("Dto", "") == m.Name);

        if (modelType != null)
        {
            cfg.CreateMap(modelType, dtoType);
        }
    }
});

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

// Auto-register all Repositories and Services using reflection
var assembly = typeof(Program).Assembly;

// Register Base Repositories for all entity types
var entityTypes = typeof(BaseEntity).Assembly.GetTypes()
    .Where(t => t.IsSubclassOf(typeof(BaseEntity)))
    .ToList();

foreach (var entityType in entityTypes)
{
    var baseRepoType = typeof(BaseRepository<>).MakeGenericType(entityType);
    var baseRepoInterfaceType = typeof(IBaseRepository<>).MakeGenericType(entityType);
    builder.Services.AddScoped(baseRepoInterfaceType, baseRepoType);
}

// Register Specific Repositories
var repositoryTypes = assembly.GetTypes()
    .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Repository") && t != typeof(BaseRepository<>))
    .ToList();

foreach (var repoType in repositoryTypes)
{
    var interfaceType = repoType.GetInterfaces()
        .FirstOrDefault(i => i.Name == "I" + repoType.Name);

    if (interfaceType != null)
    {
        builder.Services.AddScoped(interfaceType, repoType);
    }
}

// Register Services
var serviceTypes = assembly.GetTypes()
    .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Service") && !t.Name.StartsWith("Base"))
    .ToList();

foreach (var serviceType in serviceTypes)
{
    var interfaceType = serviceType.GetInterfaces()
        .FirstOrDefault(i => i.Name == "I" + serviceType.Name);

    if (interfaceType != null)
    {
        builder.Services.AddScoped(interfaceType, serviceType);
    }
}

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
