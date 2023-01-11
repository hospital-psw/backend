namespace HospitalAPI.Dto.SchedulingEvents
{
    using HospitalAPI.Dto.SchedulingEvents.Enum;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DateSelectedDto : SchedulingEventDto
    {
        [Required]
        public int PatientId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public DateSelectedDto(int aggregateId, SchedulingEventType eventType, DateTime timeStamp, int patientId, DateTime date) : base(aggregateId, eventType, timeStamp)
        {
            PatientId = patientId;
            Date = date;
        }
    }
}
