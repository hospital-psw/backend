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
        public void Create_medical_treatment()
        {

            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            NewMedicalTreatmentDto newMedicalTreatmentDto = new NewMedicalTreatmentDto
            {
                DoctorId = 4,
                PatientId = 2,
                RoomId = 1
            };

            var result = ((OkObjectResult)controller.Create(newMedicalTreatmentDto)).Value as MedicalTreatmentDto;

            Assert.NotNull(result);
            Assert.Equal("Djankarlo", result.Doctor.FirstName);
            Assert.Equal("1511", result.Room.Number);
            Assert.Equal("Matkovic", result.Patient.LastName);
        }

        [Theory]
        [ClassData(typeof(PatientReleaseData))]
        public void Release_patient_from_treatment(PatientReleaseDto dto, IActionResult expectedResult)
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = controller.ReleasePatient(dto);

            Assert.Equal(expectedResult.GetType(), result.GetType());
        }

        class PatientReleaseData : TheoryData<PatientReleaseDto, IActionResult>
        {
            public PatientReleaseData()
            {
                Add(new PatientReleaseDto("Pacijent je ok", 1), new OkObjectResult(1));
                Add(new PatientReleaseDto("", 1), new BadRequestObjectResult("Please specify reason for release"));
                Add(null, new BadRequestObjectResult("Invalid request"));
                Add(new PatientReleaseDto("Pacijent je ok", 3), new NotFoundObjectResult("Treatment not found"));
                Add(new PatientReleaseDto("Evo ga buraz", 2), new BadRequestObjectResult("Treatment is already finished"));
            }
        }


    }
}
