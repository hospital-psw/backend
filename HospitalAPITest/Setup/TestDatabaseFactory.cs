namespace HospitalAPITest.Setup
{
    using HospitalAPI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Settings;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using MySql.Data.EntityFrameworkCore.Infrastructure;

    public class TestDatabaseFactory<TStartup> : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                using var scope = BuildServiceProvider(services).CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<HospitalDbContext>();

                InitializeDatabase(db);
            });
        }

        private static ServiceProvider BuildServiceProvider(IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<HospitalDbContext>));
            services.Remove(descriptor);

            services.AddDbContext<HospitalDbContext>(opt => opt.UseMySQL(CreateConnectionStringForTest()));
            return services.BuildServiceProvider();
        }

        private static string CreateConnectionStringForTest()
        {
            return "Host=localhost;Database=HospitalApiTestDb;Username=root;Password=psw;";
        }

        private static void InitializeDatabase(HospitalDbContext context)
        {
            context.Database.EnsureDeleted();
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
