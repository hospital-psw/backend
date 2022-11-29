using static IdentityServer4.Models.IdentityResources;

namespace HospitalLibraryTest.InMemoryRepositories
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Model.Medicament;
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
            ApplicationDoctor doc1 = new ApplicationDoctor("Milos", "Gravara", "gravara@gmail.com", Specialization.GENERAL, null, null); 
            doc1.Id = 1;
            ApplicationDoctor doc2 = new ApplicationDoctor("Vuk", "Milanovic", "ckepa@gmail.com", Specialization.GENERAL, null, null);
            doc2.Id = 2;
            ApplicationDoctor doc4 = new ApplicationDoctor("Ilija", "Galin", "iki@gmail.com", Specialization.GENERAL, null, null);
            doc4.Id = 4;
            Appointment app1 = new Appointment(new DateTime(2022, 11, 22, 10, 30, 0), ExaminationType.GENERAL, null, null, doc1);
            app1.Id = 1;
            Appointment app2 = new Appointment(new DateTime(2022, 11, 23, 11, 30, 0), ExaminationType.GENERAL, null, null, doc1);
            app2.Id = 2;
            Appointment app3 = new Appointment(new DateTime(2022, 11, 24, 12, 0, 0), ExaminationType.GENERAL, null, null, doc1);
            app3.Id = 3;
            Appointment app6 = new Appointment(new DateTime(2022, 11, 22, 10, 30, 0), ExaminationType.GENERAL, null, null, doc2);
            app1.Id = 6;
            Appointment app7 = new Appointment(new DateTime(2022, 11, 22, 10, 30, 0), ExaminationType.GENERAL, null, null, doc4);
            app1.Id = 7;
            Appointment app4 = new Appointment(new DateTime(2022, 11, 24, 12, 0, 0), ExaminationType.GENERAL, null, null, doc2);
            app3.Id = 5;

            List<Appointment> appointments = new List<Appointment>();
            appointments.Add(app1);
            appointments.Add(app2);
            appointments.Add(app3);
            appointments.Add(app4);
            appointments.Add(app6);
            appointments.Add(app7);

            return appointments.Where(x => x.Doctor.Id == doctorId);
        }

        public IEnumerable<Appointment> GetAppointmentsForPatient(int patientId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Appointment> GetAppointmentsInDateRangeDoctor(int doctorId, DateTime from, DateTime to)
        {
            return GetAppointmentsForDoctor(doctorId).Where(x => from <= x.Date && to >= x.Date);
        }

        public IEnumerable<Appointment> GetScheduledAppointments(int doctorId, int patientId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Appointment> GetScheduledAppointmentsForRoom(int roomId)
        {
            List<Appointment> appointments = new List<Appointment>();
            ApplicationPatient patient = new ApplicationPatient("ana", "vulin", "vulinana@gmail.com", false, new List<Allergies>());
            Room room = new Room(1, "001", null, null, null);
            WorkingHours doctorWorkingHours = new WorkingHours(new DateTime(2022, 12, 12, 7, 0, 0), new DateTime(2022, 12, 12, 15, 0, 0));
            ApplicationDoctor doctor = new ApplicationDoctor("nikolina", "nikolic", "nina@gmail.com", Specialization.GENERAL, doctorWorkingHours, room);
            appointments.Add(new Appointment(new DateTime(2022, 12, 11, 13, 0, 0), ExaminationType.OPERATION, room, patient, doctor));

            List<Appointment> retList = new List<Appointment>();
            foreach (Appointment appointment in appointments)
            {
                if (appointment.Room.Id == roomId) retList.Add(appointment);
            }

            return retList;
        }

        public bool IsDoctorAvailable(int doctorId, DateTime date)
        {
            return !GetAppointmentsForDoctor(doctorId).Where(x => x.Date == date).Any();
        }

        public IEnumerable<Appointment> GetThisYearsAppointments()
        {
            throw new NotImplementedException();
        }


        public void Update(Appointment entity)
        {
            throw new NotImplementedException();
        }
    }
}
