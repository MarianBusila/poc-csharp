using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCoreCosmosDb
{
    public class JobContext : DbContext
    {
       
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Resource> Resources { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseCosmos("https://localhost:8081/", "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==", "JobsEFCore" )
                .EnableSensitiveDataLogging()
                .UseLoggerFactory(GenerateLoggerFactory());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Step 1
            
            //modelBuilder.HasDefaultContainer("Jobs");
            
            modelBuilder.Entity<Job>().OwnsOne(j => j.Address);
            modelBuilder.Entity<Job>().OwnsMany(j => j.Contacts);
            modelBuilder.Entity<Job>().HasOne(j => j.AssignedResource);
                
            // Step 3
            
            modelBuilder.Entity<Job>().HasPartitionKey(j => j.Department);
            modelBuilder.Entity<Job>().ToContainer("Jobs");

            modelBuilder.Entity<Resource>().HasPartitionKey(r => r.Country);
            modelBuilder.Entity<Resource>().ToContainer("Resources");
            
        }

        private ILoggerFactory GenerateLoggerFactory()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder => builder.AddConsole().AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Trace));

            return serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>();
        }

    }
}
