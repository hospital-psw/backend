namespace HospitalLibrary.Core.Repository.Core
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Domain;
    using HospitalLibrary.Core.Model.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IAppointmentRepository : IBaseRepository<Appointment>
    {

        public IEnumerable<Appointment> GetAppointmentsForPatient(int patientId);
        public IEnumerable<Appointment> GetAppointmentsForDoctor(int doctorId);
        public IEnumerable<Appointment> GetScheduledAppointments(int doctorId, int patientId);
        public IEnumerable<Appointment> GetThisYearsAppointments();
        IEnumerable<Appointment> GetScheduledAppointmentsForRoom(int roomId);

        public IEnumerable<Appointment> GetAppointmentsInDateRangeDoctor(int doctorId, DateTime from, DateTime to);

        bool IsDoctorAvailable(int doctorId, DateTime date);

        public List<Appointment> GetAllForRoom(int roomId);

        List<Appointment> GetAllBySpecialization(Specialization specialization, DateRange dateRange);


        void Save();

    }
}
