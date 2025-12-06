using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using Stripe;

using VendingMachine.BLL.Service.Classes;
using VendingMachine.BLL.Service.Interfaces;
using VendingMachine.DAL.Data;
using VendingMachine.DAL.Model;
using VendingMachine.DAL.Repository.Classes;
using VendingMachine.DAL.Repository.Interfaces;
using VendingMachine.DAL.Utils;
using VendingMachine.PL.Utils;

var builder = WebApplication.CreateBuilder(args);

// ------------------------------
//        CACHING (Redis)
// ------------------------------
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "VendingMachine_";
});

// ------------------------------
//     Dependency Injection
// ------------------------------
builder.Services.AddScoped<IFileService, FileService>(); // Your service
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services.AddScoped<ISeedData, SeedData>();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>(); // From your branch
builder.Services.AddScoped<IEmailSender, EmailSender>(); // From main branch
builder.Services.AddScoped<ICheckOutService, CheckOutService>(); // From main branch

// ------------------------------
//        STRIPE CONFIG
// ------------------------------
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

// ------------------------------
//          DATABASE
// ------------------------------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Configuration.AddUserSecrets<Program>();

// ------------------------------
//            IDENTITY
// ------------------------------
builder.Services.AddIdentity<Users, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;

    options.User.RequireUniqueEmail = true;

    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders()
.AddSignInManager()
.AddRoles<IdentityRole>();

// ------------------------------
//      AUTHENTICATION (JWT)
// ------------------------------
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])
        )
    };
});

// ------------------------------
//           CONTROLLERS
// ------------------------------
builder.Services.AddControllers();

// OpenAPI (Swagger)
builder.Services.AddOpenApi();

var app = builder.Build();

// ------------------------------
//  HTTP Request Pipeline
// ------------------------------
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// ------------------------------
//           SEEDING
// ------------------------------
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<ISeedData>();
await seeder.DataSeedingAsync();

// ------------------------------
//         MIDDLEWARE
// ------------------------------
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
