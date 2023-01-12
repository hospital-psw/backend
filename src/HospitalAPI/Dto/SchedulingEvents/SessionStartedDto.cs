namespace HospitalAPI.Dto.SchedulingEvents
{
    using HospitalAPI.Dto.SchedulingEvents.Enum;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SessionStartedDto : SchedulingEventDto
    {
        [Required]
        public int PatientId { get; set; }
        public SessionStartedDto(int aggregateId, SchedulingEventType eventType, DateTime timeStamp, int patientId) : base(aggregateId, eventType, timeStamp)
        {
            PatientId = patientId;
        }
    }
}
