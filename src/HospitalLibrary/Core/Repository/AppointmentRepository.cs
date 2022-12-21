namespace HospitalLibrary.Core.Repository
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Settings;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(HospitalDbContext context) : base(context)
        {
        }

        public override IEnumerable<Appointment> GetAll()
        {
            return HospitalDbContext.Appointments.Include(x => x.Patient)
                                                 .Include(x => x.Doctor)
                                                 .ThenInclude(x => x.Office)
                                                 .Include(x => x.Room)
                                                 .ThenInclude(x => x.Floor)
                                                 .ThenInclude(x => x.Building)
                                                 .Where(x => !x.Deleted && !x.IsDone);
        }

        public override Appointment Get(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Appointment> GetAppointmentsForPatient(int patientId)
        {
            return HospitalDbContext.Appointments.Include(x => x.Patient)
                                                 .Include(x => x.Doctor)
                                                 .Where(x => x.Patient.Id == patientId && !x.IsDone)
                                                 .ToList();

        }

        public IEnumerable<Appointment> GetAppointmentsForDoctor(int doctorId)
        {
            return HospitalDbContext.Appointments.Include(x => x.Doctor)
                                                 .Include(x => x.Patient)
                                                 .Include(x => x.Room)
                                                 .ThenInclude(x => x.Floor)
                                                 .ThenInclude(x => x.Building)
                                                 .Where(x => x.Doctor.Id == doctorId && !x.Deleted)
                                                 .ToList();
        }

        public IEnumerable<Appointment> GetScheduledAppointments(int doctorId, int patientId)
        {
            return HospitalDbContext.Appointments.Include(x => x.Patient)
                                                 .Include(x => x.Doctor)
                                                 .ThenInclude(x => x.WorkHours)
                                                 .Include(x => x.Doctor)
                                                 .ThenInclude(x => x.Office)
                                                 .Where(x => !x.Deleted && !x.IsDone && (x.Patient.Id == patientId || x.Doctor.Id == doctorId))
                                                 .Distinct()
                                                 .ToList();

        }

        public IEnumerable<Appointment> GetThisYearsAppointments()  //TODO: write unit test
        {
            return HospitalDbContext.Appointments.Where(x => x.Date.Year.Equals(DateTime.Today.Year))
                                                 .Distinct()
                                                 .ToList();
        }

        public IEnumerable<Appointment> GetScheduledAppointmentsForRoom(int roomId)
        {
            return HospitalDbContext.Appointments.Include(x => x.Patient)
                                                .Include(x => x.Doctor)
                                                .ThenInclude(x => x.WorkHours)
                                                .Include(x => x.Doctor)
                                                .ThenInclude(x => x.Office)
                                                .Where(x => !x.Deleted && x.Room.Id == roomId)
                                                .OrderBy(x => x.Date)
                                                .Distinct()
                                                .ToList();

        }

        public IEnumerable<Appointment> GetAppointmentsInDateRangeDoctor(int doctorId, DateTime from, DateTime to)
        {
            return HospitalDbContext.Appointments.Include(x => x.Doctor)
                                                 .Where(x => !x.Deleted && x.Doctor.Id == doctorId && (from <= x.Date && to >= x.Date))
                                                 .ToList();
            //(from >= x.Date && to <= x.Date)
            //DateTime.Compare(x.Date, from) > 0 && DateTime.Compare(x.Date, to) < 0
            //DbFunctions.TruncateTime()
        }

        public bool IsDoctorAvailable(int doctorId, DateTime date)
        {
            return !GetAppointmentsForDoctor(doctorId).Where(x => x.Date == date).Any();
        }

        public List<Appointment> GetAllForRoom(int roomId)
        {
            return HospitalDbContext.Appointments.Include(x => x.Room)
                                               .Where(x => !x.Deleted && (x.Room.Id == roomId))
                                               .OrderBy(x => x.Date)
                                               .Distinct()
                                               .ToList();
        }

        public void Save()
        {
            HospitalDbContext.SaveChanges();
        }

        public IEnumerable<Appointment> GetYearlyAppointmentsForDoctor(int doctorId, int year)
        {
            return HospitalDbContext.Appointments.Include(x => x.Doctor)
                                                 .Include(x => x.Patient)
                                                 .Include(x => x.Room)
                                                 .ThenInclude(x => x.Floor)
                                                 .ThenInclude(x => x.Building)
                                                 .Where(x => x.Doctor.Id == doctorId && x.Date.Year == year && !x.Deleted)
                                                 .ToList();
        }
    }
}
