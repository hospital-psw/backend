namespace IntegrationAPITest.Setup
{
    using IntegrationAPI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using IntegrationLibrary.Core;
    using IntegrationLibrary.BloodBank;
    using IntegrationLibrary.Settings;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;

    public class TestDatabaseFactory<TStartup> : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                using var scope = BuildServiceProvider(services).CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<IntegrationDbContext>();

                InitializeDatabase(db);
            });
        }

        private static ServiceProvider BuildServiceProvider(IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<IntegrationDbContext>));
            services.Remove(descriptor);

            services.AddDbContext<IntegrationDbContext>(opt => opt.UseNpgsql(CreateConnectionStringForTest()));
            return services.BuildServiceProvider();
        }

        private static string CreateConnectionStringForTest()
        {
            return "Host=localhost;Database=HospitalTestDb;Username=postgres;Password=super;";
        }

        private static void InitializeDatabase(IntegrationDbContext context)
        {
            context.Database.EnsureCreated();

            //context.Database.ExecuteSqlRaw("TRUNCATE TABLE \"Rooms\";");
            //context.Rooms.Add(new Room { Id = 1, Floor = 1, Number = "11" });
            //context.Rooms.Add(new Room { Id = 2, Floor = 1, Number = "12" });
            //context.Rooms.Add(new Room { Id = 3, Floor = 2, Number = "21" });
            //context.Rooms.Add(new Room { Id = 4, Floor = 3, Number = "31" });

            context.SaveChanges();
        }
    }
}
