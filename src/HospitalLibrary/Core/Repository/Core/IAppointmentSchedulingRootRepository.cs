namespace HospitalLibrary.Core.Repository.Core
{
    using HospitalLibrary.Core.Infrastucture;
    using HospitalLibrary.Core.Model.Events.Scheduling;
    using HospitalLibrary.Core.Model.Events.Scheduling.Root;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IAppointmentSchedulingRootRepository : IBaseRepository<AppointmentSchedulingRoot>
    {
        public IEnumerable<AppointmentSchedulingRoot>GetByPatientId(int id);
        List<SessionStarted> GetSessionStartedEvent(int id);
        List<AppointmentScheduled> GetAppointmentScheduledEvent(int id);
        List<DoctorSelected> GetDoctorSelectedEvent(int id);
        List<DateSelected> GetDateSelectedEvent(int id);
        List<SpecializationSelected> GetSpecializationSelectedEvent(int id);
        List<BackClicked> GetBackClickedEvent(int id);
        List<NextClicked> GetNextClickedEvent(int id);
        List<AppointmentSelected> GetAppointmentSelectedEvent(int id);
        List<SessionStarted> GetAllSessionStarted();
        List<AppointmentScheduled> GetAllAppointmentScheduled();

    }
}
