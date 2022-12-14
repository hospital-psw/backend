namespace IntegrationAPITest.IntegrationTests
{
    using grpcServices;
    using IntegrationAPI.Controllers;
    using IntegrationAPITest.Setup;
    using IntegrationLibrary.Settings;
    using IntegrationLibrary.UrgentBloodTransfer;
    using IntegrationLibrary.UrgentBloodTransfer.Interfaces;
    using IntegrationLibrary.UrgentBloodTransfer.Model;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Shouldly;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UrgentBloodTransferIntegrationTests : BaseIntegrationTest
    {
        public UrgentBloodTransferIntegrationTests(TestDatabaseFactory factory) : base(factory) { }

        private static UrgentBloodTransferController SetupController(IServiceScope scope)
        {
            return new UrgentBloodTransferController(scope.ServiceProvider.GetRequiredService<IUrgentBloodTransferService>());
        }

        private IntegrationDbContext SetupContext(IServiceScope scope)
        {
            var context = scope.ServiceProvider.GetRequiredService<IntegrationDbContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.SaveChanges();

            return context;
        }

        [Fact]
        public void Request_Blood_Should_Return_Created()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            SetupContext(scope);
            var request = new UrgentBloodTransfer(grpcServices.BloodType.Aplus, 5, false);

            var result = controller.RequestBlood(request);

            result.ShouldNotBeNull();
            result.GetType().GetProperty("StatusCode").GetValue(result, null).ShouldBe(201);
        }

        [Fact]
        public void Request_Blood_Should_Return_NoContent()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            SetupContext(scope);
            var request = new UrgentBloodTransfer((grpcServices.BloodType)9, 5, false);

            var result = controller.RequestBlood(request);

            result.ShouldNotBeNull();
            result.GetType().GetProperty("StatusCode").GetValue(result, null).ShouldBe(204);
        }
    }
}
