using HealthCare.API.Extensions;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Asp.Versioning;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;
using Amazon;
using Kralizek.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
 
// Load environment variables for local development
if (builder.Environment.IsDevelopment())
{
    DotNetEnv.Env.Load(Path.Combine(Directory.GetCurrentDirectory(), "..", ".env"));
    builder.Configuration.AddEnvironmentVariables();
}
else
{
    // For production, load from AWS Secrets Manager
    builder.Configuration.AddSecretsManager(null,region: RegionEndpoint.USEast1);
}

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

// API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("x-api-version"),
        new MediaTypeApiVersionReader("x-api-version"));
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HealthCare API", Version = "v1" });
});

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
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    try
    {
        var dbContext = services.GetRequiredService<HealthCare.Infrastructure.Data.HealthCareDbContext>();
        var pendingMigrations = dbContext.Database.GetPendingMigrations();
        
        if (pendingMigrations.Any())
        {
            logger.LogInformation("Applying {Count} pending migrations...", pendingMigrations.Count());
            dbContext.Database.Migrate();
            logger.LogInformation("Migrations applied successfully.");
        }
        else
        {
            logger.LogInformation("Database is already up to date. Skipping migration.");
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

app.MapControllers();

app.Run();
