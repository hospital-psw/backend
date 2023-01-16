namespace HospitalAPI.Dto.SchedulingEvents
{
    using HospitalAPI.Dto.SchedulingEvents.Enum;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class BackClickedDto : SchedulingEventDto
    {
        [Required]
        public int PatientId { get; set; }
        [Required]
        public int Step { get; set; }
        public BackClickedDto(int aggregateId, SchedulingEventType eventType, DateTime timeStamp, int patientId, int step) : base(aggregateId, eventType, timeStamp)
        {
            PatientId = patientId;
            Step = step;
        }
    }
}
