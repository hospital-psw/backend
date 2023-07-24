namespace HospitalLibraryTest.UnitTests
{
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ApplicationPatientUnitTest
    {

        private ApplicationPatient SetUpPatient()
        { 
            return new ApplicationPatient()
            {
                FirstName = "Mika",
                LastName = "Mikic",
                Email = "mika@com",
                Hospitalized = true,
                Blocked = true,
                Strikes = 3,
                LoyalityPrivelege = HospitalLibrary.Core.Model.Enums.LoyalityPrivilege.UNPRIVILEGED,
            };

        }

        [Fact]
        public void Promote_Patient()
        {
            var patient = SetUpPatient();

            patient.Promote();

            Assert.Equal(patient.LoyalityPrivelege, LoyalityPrivilege.PRIVILEGED);
        }
    }
}
