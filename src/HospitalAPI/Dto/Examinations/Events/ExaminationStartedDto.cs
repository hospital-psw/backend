namespace HospitalAPI.Dto.Examinations.Events
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ExaminationStartedDto : ExaminationEventDto
    {
        [Required]
        public int AppointmentId { get; set; }

        public ExaminationStartedDto() : base() { }

        public ExaminationStartedDto(int aggregateId, ExaminationEventType eventType, DateTime timeStamp, int appointmentId, int userId) : base(aggregateId, eventType, timeStamp, userId)
        {
            AppointmentId = appointmentId;
        }
    }
}
