using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;
using System.Text.Json;
using HealthCare.Infrastructure.Data;

namespace HealthCare.Infrastructure.Design
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<HealthCareDbContext>
    {
        public HealthCareDbContext CreateDbContext(string[] args)
        {
            // Check for provider from environment variable (used by EF tools)
            var provider = System.Environment.GetEnvironmentVariable("DB_PROVIDER") ?? "SqlServer";

            // Attempt to read appsettings.json from API project first, fallback to current directory
            var possiblePaths = new[]
            {
                Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"),
                Path.Combine(Directory.GetCurrentDirectory(), "HealthCare.API", "appsettings.json"),
                Path.Combine(Directory.GetCurrentDirectory(), "..", "HealthCare.API", "appsettings.json")
            };

            string connectionString = null;

            foreach (var path in possiblePaths)
            {
                if (!File.Exists(path)) continue;
                try
                {
                    var json = File.ReadAllText(path);
                    using var doc = JsonDocument.Parse(json);
                    
                    if (doc.RootElement.TryGetProperty("DatabaseProvider", out var dp))
                    {
                        provider = dp.GetString() ?? provider;
                    }

                    if (doc.RootElement.TryGetProperty("ConnectionStrings", out var cs))
                    {
                        var key = provider.Equals("PostgreSql", System.StringComparison.OrdinalIgnoreCase) 
                            ? "PostgreSqlConnection" 
                            : "DefaultConnection";
                            
                        if (cs.TryGetProperty(key, out var def))
                        {
                            connectionString = def.GetString();
                            break;
                        }
                        else if (cs.TryGetProperty("DefaultConnection", out var def2))
                        {
                            connectionString = def2.GetString();
                            break;
                        }
                    }
                }
                catch
                {
                    // ignore parse errors and continue
                }
            }

            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = provider.Equals("PostgreSql", System.StringComparison.OrdinalIgnoreCase)
                    ? "Host=localhost;Database=HealthCareDb;Username=postgres;Password=postgres"
                    : "Server=(localdb)\\mssqllocaldb;Database=HealthCareDb;Trusted_Connection=True;MultipleActiveResultSets=true";
            }

            var optionsBuilder = new DbContextOptionsBuilder<HealthCareDbContext>();

            if (provider.Equals("PostgreSql", System.StringComparison.OrdinalIgnoreCase))
            {
                optionsBuilder.UseNpgsql(connectionString, x => x.MigrationsAssembly("HealthCare.Infrastructure"));
            }
            else
            {
                optionsBuilder.UseSqlServer(connectionString, x => x.MigrationsAssembly("HealthCare.Infrastructure"));
            }

            return new HealthCareDbContext(optionsBuilder.Options);
        }
    }
}
