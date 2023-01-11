namespace HospitalAPI.Dto.SchedulingEvents
{
    using HospitalAPI.Dto.SchedulingEvents.Enum;
    using System;
    using System.ComponentModel.DataAnnotations;

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
