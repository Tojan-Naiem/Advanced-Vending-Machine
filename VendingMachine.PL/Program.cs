
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using VendingMachine.BLL.Service.Classes;
using VendingMachine.BLL.Service.Interfaces;
using VendingMachine.BLL.StateMachine;
using VendingMachine.BLL.StateMachine.Observers;
using VendingMachine.DAL.Data;
using VendingMachine.DAL.Repository.Classes;
using VendingMachine.DAL.Repository.Interfaces;
using VendingMachine.DAL.Utils;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis"); 
    options.InstanceName = "VendingMachine_"; 
});
builder.Services.AddScoped<IFileService,FileService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISeedData, SeedData>();
builder.Services.AddSingleton<IEventPublisher, EventPublisher>();
builder.Services.AddScoped<VendingMachineService>();



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")

    ));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
var scope = app.Services.CreateScope();
var objectOfSeedData = scope.ServiceProvider.GetRequiredService<ISeedData>();
await objectOfSeedData.DataSeedingAsync();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
