namespace HospitalAPITest.IntegrationTests
{
    using HospitalAPI;
    using HospitalAPI.Controllers;
    using HospitalAPI.Dto.MedicamentTreatment;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.DTO.MedicalTreatment;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    public class MedicalIntegrationTest : BaseIntegrationTest
    {

        public MedicalIntegrationTest(TestDatabaseFactory factory) : base(factory) { }
        private static MedicalTreatmentController SetupController(IServiceScope scope)
        {
            return new MedicalTreatmentController(scope.ServiceProvider.GetRequiredService<IMedicalTreatmentService>());
        }

        [Fact]
        public void CreateMedicalTreatment()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            NewMedicalTreatmentDto newMedicalTreatmentDto = new NewMedicalTreatmentDto
            {
                DoctorId = 3,
                PatientId = 2,
                RoomId = 1
            };

            var result = ((OkObjectResult)controller.Create(newMedicalTreatmentDto)).Value as MedicalTreatmentDto;

            Assert.NotNull(result);
            Assert.Equal("Djankarlo", result.Doctor.FirstName);
            Assert.Equal("6904", result.Room.Number);
            Assert.Equal("Djuric", result.Patient.LastName);
        }

    }
}
