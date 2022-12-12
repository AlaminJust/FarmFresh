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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Newtonsoft.Json;
using Serilog;
using FarmFresh.Domain.Unity;
using TAAP.Domain.Unity;
using FarmFresh.Email.Models;
using FarmFresh.Email.Interfaces;
using FarmFresh.Email.Services;
using Taap.Email.Services;
using Taap.Email;
using Hangfire;
using Hangfire.SqlServer;
using HangfireBasicAuthenticationFilter;

var builder = WebApplication.CreateBuilder(args);

#region SeriLogger

builder.Host.UseSerilog((context, logger) => logger
        .WriteTo.Console()
        .WriteTo.File("../Logs/FarmFreshV1_Log_.txt", rollingInterval: RollingInterval.Day,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} ({MachineName},{ThreadId},{EnvironmentUserName}) {Message} {Exception} {NewLine}")
        .Enrich.WithThreadId());
#endregion SeriLogger

#region Add controller

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    });

#endregion Add controller

#region Settings

builder.Services.Configure<DbSettings>(builder.Configuration.GetSection(nameof(DbSettings)));
var dbSettings = new DbSettings();
builder.Configuration.Bind("DbSettings", dbSettings);
builder.Services.AddSingleton(dbSettings);


var mailSettings = new MailSettings();
builder.Configuration.Bind("MailSettings", mailSettings);
builder.Services.AddSingleton(mailSettings);

#endregion Setting

#region Hangfire Configuration

builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(dbSettings.DbConnectionString, new SqlServerStorageOptions
    {
        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
        QueuePollInterval = TimeSpan.Zero,
        UseRecommendedIsolationLevel = true,
        UsePageLocksOnDequeue = true,
        DisableGlobalLocks = true
    }));

#endregion Hangfire Configuration

#region JWT Authentication

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
        };
    });

#endregion

#region Email Services

builder.Services.AddSingleton<IEnviroment, Enviroment>();

if (mailSettings.EnableMailService || builder.Environment.IsProduction())
{
    builder.Services.AddScoped<IEmailService, EmailService>();
}
else
{
    builder.Services.AddScoped<IEmailService, FileEmailService>();
}

builder.Services.AddScoped<TemplateGenerator>();
builder.Services.AddScoped<ITemplateService, TemplateService>();


#endregion Email Services

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
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
builder.Services.AddScoped<IVoucherRepository, VoucherRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
#endregion Dependency Injection for repository

#region Dependency Injection for service
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<ICartItemService, CartItemService>();
builder.Services.AddScoped<IVoucherService, VoucherService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();
builder.Services.AddScoped<ICartService, CartService>();
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

#region SPA
app.UseStaticFiles();
app.MapFallbackToFile("index.html");
#endregion SPA

#region Hangfire Dashboard

app.UseHangfireDashboard("/jobs", new DashboardOptions
{
    AppPath = "farmfresh.com",
    DashboardTitle = "Farm fresh",
    Authorization = new[]
    {
        new HangfireCustomBasicAuthenticationFilter{
            User = app.Configuration.GetSection("HangfireSettings:UserName").Value,
            Pass = app.Configuration.GetSection("HangfireSettings:Password").Value
        }
    }

});

#endregion Hangfire Dashboard

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

#region Hangfire endpoints

app.UseEndpoints(endpoints =>
{
    endpoints.MapHangfireDashboard();
});

#endregion Hangfire endpoints

app.Run();

#endregion Middleware pipeline