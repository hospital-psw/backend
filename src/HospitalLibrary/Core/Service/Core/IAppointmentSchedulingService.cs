namespace HospitalLibrary.Core.Service.Core
{
    using HospitalLibrary.Core.Model.Events.Scheduling;
    using HospitalLibrary.Core.Model.Events.Scheduling.Root;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IAppointmentSchedulingService : IBaseService<AppointmentSchedulingRoot>
    {
        AppointmentSchedulingRoot StartSession(SessionStarted evt);
        AppointmentSchedulingRoot ClickNext(NextClicked evt);
        AppointmentSchedulingRoot ClickBack(BackClicked evt);
        AppointmentSchedulingRoot SelectDate(DateSelected dateSelected);
        AppointmentSchedulingRoot SelectSpecialization(SpecializationSelected specializationSelected);
        AppointmentSchedulingRoot SelectDoctor(DoctorSelected doctorSelected);
        AppointmentSchedulingRoot SelectAppointment(AppointmentSelected appointmentSelected);
        AppointmentSchedulingRoot ScheduleAppointment(AppointmentScheduled appointmentScheduled);
        List<double> CalculateAverageTimeSpentToCreateAppointment();
        List<SessionStarted> GetAllSessionStarted();
        List<double> CalculateTheAverageNumberOfStepsToCreateAppointment();
        List<double> CalculateNumberOfTimesSpentOnEachStep();
    }
}
