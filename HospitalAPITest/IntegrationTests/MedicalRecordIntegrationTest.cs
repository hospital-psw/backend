namespace HospitalAPITest.IntegrationTests
{
    using HospitalAPI.Controllers.MedicalRecordSynchronization;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.Service.MedicalRecordSynchronization;
    using HospitalLibraryTest.InMemoryRepositories;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MedicalRecordIntegrationTest :BaseIntegrationTest
    {
        public MedicalRecordIntegrationTest(TestDatabaseFactory factory) : base(factory) { }

        private MedicalRecordSynchronizationController SetUpController()
        {
            var unitOfWork = new InMemoryUnitOfWork();
            var medicalRecordSynchronizationService = unitOfWork.MedicalRecordSynchronizationService;
            return new MedicalRecordSynchronizationController(medicalRecordSynchronizationService);
        }

        

        [Fact]
        public void Get_previous_record()
        { 
            var controller = SetUpController();

            var result = controller.GetPreviousMedicalRecord(1);

            Assert.NotNull(result);
            Assert.Equal(result.Result, "mokovani testni rekord");
        }
    }
}
