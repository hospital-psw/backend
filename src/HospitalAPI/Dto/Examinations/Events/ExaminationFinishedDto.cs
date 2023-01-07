namespace HospitalAPI.Dto.Examinations.Events
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ExaminationFinishedDto : ExaminationEventDto
    {
        [Required]
        public int AppointmentId { get; set; }

        public ExaminationFinishedDto()
        {

        }

        public ExaminationFinishedDto(int aggregateId, ExaminationEventType eventType, DateTime timeStamp, int appointmentId)
            : base(aggregateId, eventType, timeStamp)
        {
            AppointmentId = appointmentId;
        }
    }
}
