using FarmFresh.Application.Configuration;
using FarmFresh.Application.Interfaces.Services.Users;
using FarmFresh.Domain.RepoInterfaces.Users;
using FarmFresh.Infrastructure.Data;
using FarmFresh.Infrastructure.Repo.Repositories.Users;
using FarmFresh.Infrastructure.Service.Services.Users;

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

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Dependency Injection for repository
builder.Services.AddScoped<IUserRepository, UserRepository>();
#endregion Dependency Injection for repository

#region Dependency Injection for service
builder.Services.AddScoped<IUserService, UserService>();
#endregion Dependency Injection for service

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
