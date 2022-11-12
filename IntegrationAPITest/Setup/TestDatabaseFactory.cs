namespace IntegrationAPITest.Setup
{
    using IntegrationAPI;
    using IntegrationLibrary.BloodBank;
    using IntegrationLibrary.Settings;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;

    public class TestDatabaseFactory : WebApplicationFactory<Startup>
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

            services.AddDbContext<IntegrationDbContext>(opt => opt.UseSqlServer(CreateConnectionStringForTest()));
            return services.BuildServiceProvider();
        }

        private static string CreateConnectionStringForTest()
        {
            return "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=IntegrationTestBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        private static void InitializeDatabase(IntegrationDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.BloodBanks.Add(new BloodBank()
            {
                /*Id = 7, 
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
                Deleted = false,*/
                Name = "Bank 1",
                ApiUrl = "localhost:8081",
                ApiKey = "blah",
                AdminPassword = "4321",
                Email = "zika@hotmail.com",
                IsDummyPassword = true,
                GetBloodTypeAvailability = "api/checkblood/!BLOOD_TYPE",
                GetBloodTypeAndAmountAvailability = "api/checkbloodamount/!BLOOD_TYPE/!AMOUNT"
            });

            context.SaveChanges();
        }
    }
}
