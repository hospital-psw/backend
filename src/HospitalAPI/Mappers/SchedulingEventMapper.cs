namespace HospitalAPI.Mappers
{
    using HospitalAPI.Dto.SchedulingEvents;
    using HospitalLibrary.Core.Model.Events.Scheduling;
    using HospitalLibrary.Core.Model.Events.Scheduling.Root;
    using IdentityServer4.Events;

    public class SchedulingEventMapper
    {
        public static SessionStarted SessionStartedDtoToEntity(SessionStartedDto dto)
        {
            return new SessionStarted(dto.AggregateId, dto.TimeStamp, dto.EventType.ToString(), dto.PatientId);
        }
        public static NextClicked NextDtoToEntity(NextClickedDto dto)
        {
            return new NextClicked(dto.AggregateId, dto.TimeStamp, dto.EventType.ToString(), dto.Step, dto.PatientId);
        }
        public static BackClicked BackDtoToEntity(BackClickedDto dto)
        {
            return new BackClicked(dto.AggregateId, dto.TimeStamp, dto.EventType.ToString(), dto.Step, dto.PatientId);
        }
        public static DateSelected DateSelectedDtoToEntity(DateSelectedDto dto)
        {
            return new DateSelected(dto.AggregateId, dto.TimeStamp, dto.EventType.ToString(), dto.Date, dto.PatientId);
        }
        public static SpecializationSelected SpecializationSelectedDtoToEntity(SpecializationSelectedDto dto)
        {
            return new SpecializationSelected(dto.AggregateId, dto.TimeStamp, dto.EventType.ToString(), dto.Specialization, dto.PatientId);
        }
        public static DoctorSelected DoctorSelectedDtoToEntity(DoctorSelectedDto dto)
        {
            return new DoctorSelected(dto.AggregateId, dto.TimeStamp, dto.EventType.ToString(), dto.DoctorId, dto.PatientId);
        }
        public static AppointmentSelected AppointmentSelectedDtoToEntity(AppointmentSelectedDto dto)
        {
            return new AppointmentSelected(dto.AggregateId, dto.TimeStamp, dto.EventType.ToString(), dto.DateTime, dto.PatientId);
        }
        public static AppointmentScheduled AppointmentScheduledDtoToEntity(AppointmentScheduledDto dto)
        {
            return new AppointmentScheduled(dto.AggregateId, dto.TimeStamp, dto.EventType.ToString(), dto.PatientId);
        }
        public static SchedulingRootDto EntityToDto(AppointmentSchedulingRoot root)
        {
            SchedulingRootDto dto = new SchedulingRootDto();
            dto.Id = root.Id;
            dto.LastChanged = root.LastChange;
            dto.PatientId = root.PatientId;
            dto.DoctorId = (root.DoctorId is not null) ? root.DoctorId : null;
            dto.Specialization = (root.Specialization is not null) ? root.Specialization : null;
            dto.DatePicked = (root.Date is not null) ? root.Date : null;
            dto.TimePicked = (root.Time is not null) ? root.Time : null;

            return dto;
        }
    }
}
