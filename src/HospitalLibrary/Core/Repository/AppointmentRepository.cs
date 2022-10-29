namespace HospitalLibrary.Core.Repository
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Settings;
    using Microsoft.EntityFrameworkCore;
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

        public Appointment GetAppointmentIfNotDone(int appointmentId)
        {
            return HospitalDbContext.Appointments.Where(x => !x.IsDone && x.Id == appointmentId).FirstOrDefault();
        }

        public override IEnumerable<Appointment> GetAll()
        {
            return HospitalDbContext.Appointments.Include(x => x.Patient)
                                                 .Include(x => x.Doctor)
                                                 .Where(x => !x.Deleted);
        }

        public override Appointment Get(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }
    }
}
