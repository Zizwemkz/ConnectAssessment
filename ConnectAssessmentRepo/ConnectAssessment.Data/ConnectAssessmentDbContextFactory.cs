using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.IO;

    namespace ConnectAssessment.Data
    {
        public class ConnectAssessmentDbContextFactory : IDesignTimeDbContextFactory<ConnectAssessmentDbContext>
        {
            public ConnectAssessmentDbContext CreateDbContext(string[] args)
            {
                // Set up configuration to read appsettings.json
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true)
                    .Build();

                var builder = new DbContextOptionsBuilder<ConnectAssessmentDbContext>();
                var connectionString = configuration.GetConnectionString("DefaultConnection");

                builder.UseSqlServer(connectionString);

                return new ConnectAssessmentDbContext(builder.Options);
            }
        }
    }
