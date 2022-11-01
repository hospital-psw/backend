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

            HospitalDbContext.Appointments.Where(x => x.Patient.Id == patientId);


        }

        public IEnumerable<Appointment> GetAppointmentsForDoctor(int doctorId)
        {
            return HospitalDbContext.Appointments.Include(x => x.Doctor)
                                                 .Include(x => x.Patient)
                                                 .Include(x => x.Room)
                                                 .ThenInclude(x => x.Floor)
                                                 .ThenInclude(x => x.Building)
                                                 .Where(x => x.Doctor.Id == doctorId)
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
    }
}
