namespace HospitalAPITest.IntegrationTests
{
    using HospitalAPI;
    using HospitalAPI.Controllers;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.Extensions.DependencyInjection;

    public class MedicalIntegrationTest : BaseIntegrationTest
    {

        public MedicalIntegrationTest(TestDatabaseFactory factory) : base(factory) { }
        private static MedicalTreatmentController SetupController(IServiceScope scope)
        {
            return new MedicalTreatmentController(scope.ServiceProvider.GetRequiredService<IMedicalTreatmentService>());
        }

        [Fact]
        public void Test()
        {
            //using var scope = Factory.Services.CreateScope();
            //var controller = SetupController(scope);

            

        }
    }
}
