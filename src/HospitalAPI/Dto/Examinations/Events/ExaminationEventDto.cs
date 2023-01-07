namespace HospitalAPI.Dto.Examinations.Events
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ExaminationEventDto
    {
        [Required]
        public int AggregateId { get; set; }

        [Required]
        public ExaminationEventType EventType { get; set; }

        [Required]
        public DateTime TimeStamp { get; set; }

        public ExaminationEventDto() { }

        public ExaminationEventDto(int aggregateId, ExaminationEventType eventType, DateTime timeStamp)
        {
            AggregateId = aggregateId;
            EventType = eventType;
            TimeStamp = timeStamp;
        }
    }
}
