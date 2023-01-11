namespace HospitalAPI.Dto.SchedulingEvents
{
    using HospitalAPI.Dto.SchedulingEvents.Enum;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class NextClickedDto : SchedulingEventDto
    {
        [Required]
        public int Step { get; set; }
        [Required]
        public int PatientId { get; set; }
        public NextClickedDto(int aggregateId, SchedulingEventType eventType, DateTime timeStamp, int step, int patientId) : base(aggregateId, eventType, timeStamp)
        {
            Step = step;
            PatientId = patientId;
        }
    }
}
