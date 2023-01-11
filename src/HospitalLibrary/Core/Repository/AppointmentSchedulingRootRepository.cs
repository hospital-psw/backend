namespace HospitalLibrary.Core.Repository
{
    using HospitalLibrary.Core.Infrastucture;
    using HospitalLibrary.Core.Model.Events.Scheduling;
    using HospitalLibrary.Core.Model.Events.Scheduling.Root;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Settings;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AppointmentSchedulingRootRepository : BaseRepository<AppointmentSchedulingRoot>, IAppointmentSchedulingRootRepository
    {
        public AppointmentSchedulingRootRepository(HospitalDbContext context) : base(context)
        {
        }

        public IEnumerable<AppointmentSchedulingRoot> GetByPatientId(int id)
        {
            return HospitalDbContext.AppointmentRoots.Where(x => x.PatientId == id && x.Completed == false);
        }
        public override List<AppointmentSchedulingRoot> GetAll()
        {
            return HospitalDbContext.AppointmentRoots.ToList();
        }
        public List<SessionStarted> GetSessionStartedEvent(int id)
        {
            return HospitalDbContext.SessionStartedEvents.Where(x => x.AggregateId == id).ToList();
        }
        public List<AppointmentScheduled> GetAppointmentScheduledEvent(int id)
        {
            return HospitalDbContext.AppointmentScheduledEvents.Where(x => x.AggregateId == id).ToList();
        }
        public List<SessionStarted> GetAllSessionStarted()
        {
            return HospitalDbContext.SessionStartedEvents.OrderBy(x => x.DateCreated).ToList();
        }
        public List<AppointmentScheduled> GetAllAppointmentScheduled()
        {
            return HospitalDbContext.AppointmentScheduledEvents.OrderBy(x => x.DateCreated).ToList();
        }

        public List<DoctorSelected> GetDoctorSelectedEvent(int id)
        {
            return HospitalDbContext.DoctorSelectedEvents.Where(x => x.AggregateId == id).ToList();
        }

        public List<DateSelected> GetDateSelectedEvent(int id)
        {
            return HospitalDbContext.DateSelectedEvents.Where(x => x.AggregateId == id).ToList();
        }

        public List<SpecializationSelected> GetSpecializationSelectedEvent(int id)
        {
            return HospitalDbContext.SpecializationSelectedEvents.Where(x => x.AggregateId == id).ToList();
        }

        public List<BackClicked> GetBackClickedEvent(int id)
        {
            return HospitalDbContext.BackClickedEvents.Where(x => x.AggregateId == id).ToList();
        }

        public List<NextClicked> GetNextClickedEvent(int id)
        {
            return HospitalDbContext.NextClickedEvents.Where(x => x.AggregateId == id).ToList();
        }

        public List<AppointmentSelected> GetAppointmentSelectedEvent(int id)
        {
            return HospitalDbContext.AppointmentSelectedEvents.Where(x => x.AggregateId == id).ToList();
        }
    }
}
