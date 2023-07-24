namespace HospitalAPITest.IntegrationTests
{
    using HospitalAPI.Controllers.MedicalRecordSynchronization;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.Service.MedicalRecordSynchronization;
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
            var stub = new Mock<MedicalRecordSynchronizationService> { CallBase = true };
            stub.Setup(x => x.GetPreviousMedicalRecord(1))
                .Returns(Task.FromResult("testni rekord"));
            return new MedicalRecordSynchronizationController(stub.Object);
        }

        [Fact]
        public void Get_previous_record()
        { 
            var controller = SetUpController();

            var result = controller.GetPreviousMedicalRecord(1);

            Assert.NotNull(result);
            Assert.Equal(result.Result, "testni rekord");
        }
    }
}
