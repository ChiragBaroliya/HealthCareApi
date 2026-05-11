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
            // Attempt to read appsettings.json from API project first, fallback to current directory
            var possiblePaths = new[]
            {
                Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"),
                Path.Combine(Directory.GetCurrentDirectory(), "..", "HealthCare.API", "appsettings.json"),
                Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "HealthCare.API", "appsettings.json")
            };

            string connectionString = null;

            foreach (var path in possiblePaths)
            {
                if (!File.Exists(path)) continue;
                try
                {
                    var json = File.ReadAllText(path);
                    using var doc = JsonDocument.Parse(json);
                    if (doc.RootElement.TryGetProperty("ConnectionStrings", out var cs) && cs.TryGetProperty("DefaultConnection", out var def))
                    {
                        connectionString = def.GetString();
                        break;
                    }
                }
                catch
                {
                    // ignore parse errors and continue
                }
            }

            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = "Server=(localdb)\\mssqllocaldb;Database=HealthCareDb;Trusted_Connection=True;MultipleActiveResultSets=true";
            }

            var optionsBuilder = new DbContextOptionsBuilder<HealthCareDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new HealthCareDbContext(optionsBuilder.Options);
        }
    }
}
