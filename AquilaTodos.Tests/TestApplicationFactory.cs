namespace AquilaTodos.Tests
{
    using System;
    using System.IO;
    using System.Linq;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public abstract class TestApplicationFactory : WebApplicationFactory<Program>
    {
        public TestServer TestServer { get; set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var directory = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(directory, "appsettings.json");

            builder.ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile(configPath, false, false);
            });

            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<TodoContext>));
                services.Remove(descriptor);

                var provider = this.AddDatabaseService(services)
                    .BuildServiceProvider();

                services.AddDbContext<TodoContext>(options =>
                {
                    this.AddDatabase(options);
                    options.UseInternalServiceProvider(provider);
                    options.EnableSensitiveDataLogging();
                });
            });

            builder.ConfigureTestServices(services =>
            {

                var provider = services.BuildServiceProvider();

                using var scope = provider.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<TodoContext>();
                db.Database.EnsureCreated();
                this.SeedData(db);
                db.SaveChanges();
            });
        }

        protected virtual IServiceCollection AddDatabaseService(IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddEntityFrameworkInMemoryDatabase();
        }

        protected virtual void AddDatabase(DbContextOptionsBuilder options)
        {
            var databaseName = $"database{this.GetType().Name}";
            options.UseInMemoryDatabase(databaseName);
        }

        protected virtual DateTime Now()
        {
            return DateTime.UtcNow;
        }

        protected abstract void SeedData(TodoContext context);

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            this.TestServer?.Dispose();
        }
    }
}
