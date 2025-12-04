
using VendingMachine.DAL.Utils;


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddScoped<ISeedData, SeedData>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddOpenApi();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
