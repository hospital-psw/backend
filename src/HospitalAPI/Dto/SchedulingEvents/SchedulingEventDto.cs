namespace HospitalAPI.Dto.SchedulingEvents
{
    using System.ComponentModel.DataAnnotations;
    using System;
    using HospitalAPI.Dto.SchedulingEvents.Enum;

    public class SchedulingEventDto
    {
        [Required]
        public int AggregateId { get; set; }

        [Required]
        public SchedulingEventType EventType { get; set; }

        [Required]
        public DateTime TimeStamp { get; set; }

        public SchedulingEventDto(int aggregateId, SchedulingEventType eventType, DateTime timeStamp)
        {
            AggregateId = aggregateId;
            EventType = eventType;
            TimeStamp = timeStamp;
        }
    }
}
