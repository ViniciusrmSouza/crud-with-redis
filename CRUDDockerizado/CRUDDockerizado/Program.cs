using CRUDDockerizado.CRUDDockerizado.Application.Services.Implementations;
using CRUDDockerizado.CRUDDockerizado.Application.Services.Interfaces;
using CRUDDockerizado.CRUDDockerizado.Domain.Caching;
using CRUDDockerizado.CRUDDockerizado.Domain.Repository;
using CRUDDockerizado.CRUDDockerizado.Infrastructure.Caching;
using CRUDDockerizado.CRUDDockerizado.Infrastructure.Data;
using CRUDDockerizado.CRUDDockerizado.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CrudContext>(opt =>
    opt.UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 35))));

builder.Services.AddScoped<ICachingService, CachingService>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IInventoryService, IventoryService>();

builder.Services.AddStackExchangeRedisCache(x =>
{
    x.InstanceName = "instance";
    x.Configuration = "localhost:6379";
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});


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