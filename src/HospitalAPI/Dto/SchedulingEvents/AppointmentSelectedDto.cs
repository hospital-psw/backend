namespace HospitalAPI.Dto.SchedulingEvents
{
    using HospitalAPI.Dto.SchedulingEvents.Enum;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AppointmentSelectedDto : SchedulingEventDto
    {
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public int PatientId { get; set; }
        public AppointmentSelectedDto(int aggregateId, SchedulingEventType eventType, DateTime timeStamp, DateTime dateTime, int patientId) : base(aggregateId, eventType, timeStamp)
        {
            DateTime = dateTime;
            PatientId = patientId;
        }
    }
}
