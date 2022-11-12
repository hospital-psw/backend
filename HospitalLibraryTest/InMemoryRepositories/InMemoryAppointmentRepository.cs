namespace HospitalLibraryTest.InMemoryRepositories
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Repository.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class InMemoryAppointmentRepository : IAppointmentRepository
    {
        public void Add(Appointment entity)
        {
            throw new NotImplementedException();
        }

        public Appointment Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Appointment> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Appointment> GetAppointmentsForDoctor(int doctorId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Appointment> GetAppointmentsForPatient(int patientId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Appointment> GetScheduledAppointments(int doctorId, int patientId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Appointment> GetScheduledAppointmentsForRoom(int roomId)
        {
            List<Appointment> appointments = new List<Appointment>();
            Patient patient = new Patient("ana", "vulin", "vulinana@gmail.com", "123", false);
            Room room = new Room(1, "001", null, null, null);
            WorkingHours doctorWorkingHours = new WorkingHours(new DateTime(2022, 12, 12, 7, 0, 0), new DateTime(2022, 12, 12, 15, 0, 0));
            Doctor doctor = new Doctor("nikolina", "nikolic", "nina@gmail.com", "123", Specialization.GENERAL, doctorWorkingHours, room);
            appointments.Add(new Appointment(new DateTime(2022, 12, 12, 13, 0, 0), ExaminationType.OPERATION, room, patient, doctor));

            return appointments;
        }

        public void Update(Appointment entity)
        {
            throw new NotImplementedException();
        }
    }
}
