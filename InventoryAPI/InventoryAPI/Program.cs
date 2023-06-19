using InventoryAPI.Contexts;
using InventoryAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<InventoryContext>(options =>
options.UseSqlServer(configuration.
GetConnectionString("InvConn")));
builder.Services.AddTransient<ICategoryRepo, CategoryRepo>();
builder.Services.AddTransient<IProductRepo, ProductRepo>(); 

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
