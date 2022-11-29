namespace HospitalLibraryTest.UnitTests
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Model.VacationRequests;
    using HospitalLibrary.Core.Service;
    using HospitalLibraryTest.InMemoryRepositories;
    using Microsoft.Extensions.Logging;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EmergencyVacationRequestUnitTest
    {

        public VacationRequestsService SetupService()
        {
            var logger = new Mock<ILogger<VacationRequest>>();
            return new VacationRequestsService(logger.Object, new InMemoryUnitOfWork());
        }

        [Theory]
        [ClassData(typeof(AvailableSubstituteDoctorsData))]
        public void Get_available_doctors_for_appointment(Appointment appointment, int expectedId)
        {
            var service = SetupService();

            var result = service.GetAvailableDoctorOfSameSpecialization(appointment);
            int value = result != null ? result.Id : -1;

            Assert.Equal(value, expectedId);
        }

        [Theory]
        [ClassData(typeof(DoctorSubstitutionData))]
        public void Substitute_doctors(List<Appointment> appointments, bool expected)
        {
            var service = SetupService();

            var result = service.SubstituteDoctors(appointments);

            Assert.Equal(expected, result);
        }

        class AvailableSubstituteDoctorsData : TheoryData<Appointment, int>
        {
            public AvailableSubstituteDoctorsData()
            {
                ApplicationDoctor doc1 = new ApplicationDoctor("Milos", "Gravara", "gravara@gmail.com", Specialization.GENERAL, null, null, null);
                doc1.Id = 1;
                Appointment app3 = new Appointment(new DateTime(2022, 11, 24, 12, 0, 0), ExaminationType.GENERAL, null, null, doc1);
                app3.Id = 3;
                Appointment app4 = new Appointment(new DateTime(2022, 11, 22, 10, 30, 0), ExaminationType.GENERAL, null, null, doc1);
                app3.Id = 6;
                Add(app3, 4);
                Add(app4, -1);
            }
        }

        class DoctorSubstitutionData : TheoryData<List<Appointment>, bool>
        {
            public DoctorSubstitutionData()
            {
                ApplicationDoctor doc1 = new ApplicationDoctor("Milos", "Gravara", "gravara@gmail.com", Specialization.GENERAL, null, null, null);
                doc1.Id = 1;
                Appointment app1 = new Appointment(new DateTime(2022, 11, 22, 10, 30, 0), ExaminationType.GENERAL, null, null, doc1);
                app1.Id = 1;
                Appointment app2 = new Appointment(new DateTime(2022, 11, 23, 11, 30, 0), ExaminationType.GENERAL, null, null, doc1);
                app2.Id = 2;
                Appointment app3 = new Appointment(new DateTime(2022, 11, 24, 12, 0, 0), ExaminationType.GENERAL, null, null, doc1);
                app3.Id = 3;
                List<Appointment> appointments1 = new List<Appointment>();
                appointments1.Add(app2);
                appointments1.Add(app3);
                List<Appointment> appointments2 = new List<Appointment>();
                appointments2.Add(app1);
                appointments2.Add(app2);
                appointments2.Add(app3);
                Add(appointments1, true);
                Add(appointments2, false);
            }
        }
    }
}
