namespace HospitalAPI.Dto.Examinations.Events
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ExaminationStartedDto : ExaminationEventDto
    {
        [Required]
        public int AppointmentId { get; set; }

        public ExaminationStartedDto() : base() { }

        public ExaminationStartedDto(int aggregateId, ExaminationEventType eventType, DateTime timeStamp, int appointmentId) : base(aggregateId, eventType, timeStamp)
        {
            AppointmentId = appointmentId;
        }
    }
}
