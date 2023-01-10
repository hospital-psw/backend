namespace HospitalAPI.Dto.Examinations.Events
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PrescriptionRemovedDto : ExaminationEventDto
    {
        [Required]
        public int PrescriptionId { get; set; }

        public PrescriptionRemovedDto()
        {

        }

        public PrescriptionRemovedDto(int aggregateId, ExaminationEventType eventType, DateTime timeStamp, int prescId, int userId)
            : base(aggregateId, eventType, timeStamp, userId)
        {
            PrescriptionId = prescId;
        }
    }
}
