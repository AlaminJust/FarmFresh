using FarmFresh.Api.Automapper;
using FarmFresh.Application.Configuration;
using FarmFresh.Application.Interfaces.Services.Products;
using FarmFresh.Application.Interfaces.Services.Users;
using FarmFresh.Domain.RepoInterfaces.Products;
using FarmFresh.Domain.RepoInterfaces.Users;
using FarmFresh.Infrastructure.Data;
using FarmFresh.Infrastructure.Repo.Repositories.Products;
using FarmFresh.Infrastructure.Repo.Repositories.Users;
using FarmFresh.Infrastructure.Service.Services.Products;
using FarmFresh.Infrastructure.Service.Services.Users;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

#region Settings

builder.Services.Configure<DbSettings>(builder.Configuration.GetSection(nameof(DbSettings)));
var dbSettings = new DbSettings();
builder.Configuration.Bind("DbSettings", dbSettings);
builder.Services.AddSingleton(dbSettings);

#endregion Setting

#region Dependency Injection for entity framework core implementation (Infrastructure)
builder.Services.AddPersistence(dbSettings.DbConnectionString);
#endregion

#region Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#endregion Add services to the container

#region Swagger configuration

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Farmfresh api", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
    //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

#endregion Swagger configuration

#region Dependency Injection for repository
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
#endregion Dependency Injection for repository

#region Dependency Injection for service
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
#endregion Dependency Injection for service

#region Automapper
builder.Services.AddAutoMapper(typeof(DefaultProfile), typeof(UserMapperProfile));
#endregion Automapper

var app = builder.Build();

#region Middleware pipeline

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion Middleware pipeline