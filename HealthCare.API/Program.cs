using HealthCare.API.Extensions;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Force Kestrel to use HTTP/1.1 to avoid HTTP/2 protocol issues in some dev environments
builder.WebHost.ConfigureKestrel(options =>
{
    options.ConfigureEndpointDefaults(listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1;
    });
});

// Add services to the container.

builder.Services.AddControllers();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add application services and DbContext
builder.Services.AddHealthCareServices(builder.Configuration);

// Configure JWT authentication
var jwtSection = builder.Configuration.GetSection("JwtSettings");
var jwtSecret = jwtSection.GetValue<string>("Secret") ?? "ThisIsASecretKeyForJwtTokenShouldBeStoredSafely";

// Bind JwtSettings for DI
builder.Services.Configure<HealthCare.Application.Models.JwtSettings>(jwtSection);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(jwtSecret))
    };
});

var app = builder.Build();

// Enable Swagger middleware
app.UseSwagger();

// Swagger UI opens automatically at root URL (https://localhost:44305/)
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "HealthCare API V1");
    c.RoutePrefix = string.Empty;
});

// global error handling middleware
app.UseMiddleware<HealthCare.API.Middleware.GlobalErrorHandlingMiddleware>();

app.UseHttpsRedirection();

// Authentication must come before Authorization
app.UseAuthentication();
app.UseAuthorization();

// Automatically apply any pending migrations on startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<HealthCare.Infrastructure.Data.HealthCareDbContext>();
    dbContext.Database.Migrate();
}

app.MapControllers();

app.Run();
