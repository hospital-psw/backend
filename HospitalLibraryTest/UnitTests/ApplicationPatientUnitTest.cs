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
            };

        }

        [Fact]
        public void Check_rewards_for_new_patient()
        { 
            var patient = SetUpPatient();

            string result = patient.GetRewards();

            Assert.Equal(result, "Nista");
        }

        [Fact]
        public void Promote_patient()
        {
            var patient = SetUpPatient();
            patient.Promote();

            string result = patient.GetRewards();

            Assert.Equal(result, "Majica i privezak");
        }
    }
}
