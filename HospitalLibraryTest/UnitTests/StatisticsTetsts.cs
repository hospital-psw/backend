namespace HospitalLibraryTest.UnitTests
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Medicament;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service;
    using HospitalLibrary.Util;
    using Moq;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class StatisticsTests
    {
        [Fact]
        public void Finds_Monthly_Appointments_Statistics()
        {
            var stub = new Mock<IUnitOfWork>();
            List<Appointment> appointments = new List<Appointment>();
            Room room = new Room(1, "101", null, "Test", null);
            Patient patient = new Patient("Imenko", "Prezimenic", "email@email.com", "password", false, new List<Allergies>());
            Doctor doctor = new Doctor("Imenko", "Prezimenic", "password", "email@email.com", HospitalLibrary.Core.Model.Enums.Specialization.GENERAL, null, null, null);
            appointments.Add(new Appointment(new DateTime(2022, 5, 1), HospitalLibrary.Core.Model.Enums.ExaminationType.GENERAL, room, patient, doctor));
            appointments.Add(new Appointment(new DateTime(2022, 5, 2), HospitalLibrary.Core.Model.Enums.ExaminationType.GENERAL, room, patient, doctor));
            appointments.Add(new Appointment(new DateTime(2022, 3, 1), HospitalLibrary.Core.Model.Enums.ExaminationType.GENERAL, room, patient, doctor));
            stub.Setup(f => f.AppointmentRepository.GetThisYearsAppointments()).Returns(appointments);
            StatisticsService service = new StatisticsService(stub.Object);


            IEnumerable<int> result = service.GetNumberOfAppointmentsPerMonth();


            List<int> expected = ListFactory.CreateList<int>(0, 0, 1, 0, 2, 0, 0, 0, 0, 0, 0, 0);
            Assert.NotNull(result);
            Assert.Equal(result, expected);
        }
        [Fact]
        public void Finds_No_Monthly_Appointments_Statistics()
        {
            var stub = new Mock<IUnitOfWork>();
            List<Appointment> appointments = new List<Appointment>();
            stub.Setup(f => f.AppointmentRepository.GetThisYearsAppointments()).Returns(appointments);
            StatisticsService service = new StatisticsService(stub.Object);


            IEnumerable<int> result = service.GetNumberOfAppointmentsPerMonth();


            List<int> expected = ListFactory.CreateList<int>(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            Assert.NotNull(result);
            Assert.Equal(result, expected);
        }

        [Theory]
        [ClassData(typeof(DatesOfBirth))]
        public void Gets_Age_From_Date(DateTime dateOfBirth, int age)
        {
            var stub = new Mock<IUnitOfWork>();
            StatisticsService service = new StatisticsService(stub.Object);
            service.GetAge(dateOfBirth);
            Assert.Equal(age, service.GetAge(dateOfBirth));
        }
        [Theory]
        [ClassData(typeof(TestPatients))]
        public void Gets_Patients_Age_Group(ApplicationPatient patient, int ageGroup)
        {
            var stub = new Mock<IUnitOfWork>();
            StatisticsService service = new StatisticsService(stub.Object);
            service.GetAgeGroup(patient);

            Assert.Equal(ageGroup, service.GetAgeGroup(patient));
        }

        [Fact]
        public void Gets_Patients_By_Age_Group()
        {
            var stub = new Mock<IUnitOfWork>();
            List<ApplicationPatient> patients = new List<ApplicationPatient>();
            patients.Add(new ApplicationPatient("Imenko", "Prezimenic", new DateTime(2022, 4, 4), HospitalLibrary.Core.Model.Enums.Gender.MALE, false, HospitalLibrary.Core.Model.Blood.Enums.BloodType.A_PLUS));
            patients.Add(new ApplicationPatient("Imenko", "Prezimenic", new DateTime(2006, 4, 4), HospitalLibrary.Core.Model.Enums.Gender.MALE, false, HospitalLibrary.Core.Model.Blood.Enums.BloodType.A_PLUS));
            patients.Add(new ApplicationPatient("Imenko", "Prezimenic", new DateTime(1996, 4, 4), HospitalLibrary.Core.Model.Enums.Gender.MALE, false, HospitalLibrary.Core.Model.Blood.Enums.BloodType.A_PLUS));
            patients.Add(new ApplicationPatient("Imenko", "Prezimenic", new DateTime(1985, 4, 4), HospitalLibrary.Core.Model.Enums.Gender.MALE, false, HospitalLibrary.Core.Model.Blood.Enums.BloodType.A_PLUS));
            patients.Add(new ApplicationPatient("Imenko", "Prezimenic", new DateTime(1970, 4, 4), HospitalLibrary.Core.Model.Enums.Gender.MALE, false, HospitalLibrary.Core.Model.Blood.Enums.BloodType.A_PLUS));
            patients.Add(new ApplicationPatient("Imenko", "Prezimenic", new DateTime(1935, 4, 4), HospitalLibrary.Core.Model.Enums.Gender.MALE, false, HospitalLibrary.Core.Model.Blood.Enums.BloodType.A_PLUS));
            stub.Setup(f => f.ApplicationUserRepository.GetAllPatients()).Returns(patients);
            StatisticsService service = new StatisticsService(stub.Object);
            List<int> expectedMale = ListFactory.CreateList(1, 1, 1, 1, 1, 1);
            List<int> expectedFEmale = ListFactory.CreateList(0, 0, 0, 0, 0, 0);

            var (males, females) = service.GetNumberOfPatientsByAgeGroup();

            Assert.Equal(males, expectedMale);
            Assert.Equal(females, expectedFEmale);
        }

        [Fact]
        public void Gets_Number_Of_Users_By_Type()
        {
            var stub = new Mock<IUnitOfWork>();
            List<ApplicationPatient> patients = new List<ApplicationPatient>();
            patients.Add(new ApplicationPatient("Imenko", "Prezimenic", new DateTime(2022, 4, 4), HospitalLibrary.Core.Model.Enums.Gender.MALE, false, HospitalLibrary.Core.Model.Blood.Enums.BloodType.A_PLUS));
            patients.Add(new ApplicationPatient("Imenko", "Prezimenic", new DateTime(2006, 4, 4), HospitalLibrary.Core.Model.Enums.Gender.MALE, false, HospitalLibrary.Core.Model.Blood.Enums.BloodType.A_PLUS));
            List<ApplicationDoctor> doctors = new List<ApplicationDoctor>();
            doctors.Add(new ApplicationDoctor("Imenko", "Prezimenic", new DateTime(2022, 4, 4), HospitalLibrary.Core.Model.Enums.Gender.MALE, HospitalLibrary.Core.Model.Enums.Specialization.GENERAL, null, null, null));
            doctors.Add(new ApplicationDoctor("Imenko", "Prezimenic", new DateTime(2022, 4, 4), HospitalLibrary.Core.Model.Enums.Gender.MALE, HospitalLibrary.Core.Model.Enums.Specialization.CARDIOLOGY, null, null, null));
            stub.Setup(f => f.ApplicationUserRepository.GetAllDoctors()).Returns(doctors);
            stub.Setup(f => f.ApplicationUserRepository.GetAllPatients()).Returns(patients);
            StatisticsService service = new StatisticsService(stub.Object);
            List<int> expected = ListFactory.CreateList(2, 1, 1, 0);

        }
    }

    class DatesOfBirth : TheoryData<DateTime, int>
    {
        public DatesOfBirth()
        {
            Add(new DateTime(2000, 12, 26), 21);
            Add(new DateTime(2005, 5, 1), 17);
        }
    }

    class TestPatients : TheoryData<ApplicationPatient, int>
    {
        public TestPatients()
        {
            Add(new ApplicationPatient("Imenko", "Prezimenic", new DateTime(2022, 4, 4), HospitalLibrary.Core.Model.Enums.Gender.MALE, false, HospitalLibrary.Core.Model.Blood.Enums.BloodType.A_PLUS), 0);
            Add(new ApplicationPatient("Imenko", "Prezimenic", new DateTime(2006, 4, 4), HospitalLibrary.Core.Model.Enums.Gender.MALE, false, HospitalLibrary.Core.Model.Blood.Enums.BloodType.A_PLUS), 1);
            Add(new ApplicationPatient("Imenko", "Prezimenic", new DateTime(1996, 4, 4), HospitalLibrary.Core.Model.Enums.Gender.MALE, false, HospitalLibrary.Core.Model.Blood.Enums.BloodType.A_PLUS), 2);
            Add(new ApplicationPatient("Imenko", "Prezimenic", new DateTime(1985, 4, 4), HospitalLibrary.Core.Model.Enums.Gender.MALE, false, HospitalLibrary.Core.Model.Blood.Enums.BloodType.A_PLUS), 3);
            Add(new ApplicationPatient("Imenko", "Prezimenic", new DateTime(1970, 4, 4), HospitalLibrary.Core.Model.Enums.Gender.MALE, false, HospitalLibrary.Core.Model.Blood.Enums.BloodType.A_PLUS), 4);
            Add(new ApplicationPatient("Imenko", "Prezimenic", new DateTime(1935, 4, 4), HospitalLibrary.Core.Model.Enums.Gender.MALE, false, HospitalLibrary.Core.Model.Blood.Enums.BloodType.A_PLUS), 5);
        }
    }
}
