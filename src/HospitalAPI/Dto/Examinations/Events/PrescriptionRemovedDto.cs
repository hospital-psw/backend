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

        public PrescriptionRemovedDto(int aggregateId, ExaminationEventType eventType, DateTime timeStamp, int prescId)
            :base(aggregateId, eventType, timeStamp)
        {
            PrescriptionId = prescId;
        }
    }
}
