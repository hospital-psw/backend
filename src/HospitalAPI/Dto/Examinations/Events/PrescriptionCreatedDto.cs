namespace HospitalAPI.Dto.Examinations.Events
{
    using HospitalLibrary.Core.DTO.Examinations;
    using System;

    public class PrescriptionCreatedDto : ExaminationEventDto
    {
        public NewPrescriptionDto NewPrescription { get; set; }

        public PrescriptionCreatedDto()
        {

        }

        public PrescriptionCreatedDto(int aggregateId, ExaminationEventType eventType, DateTime timeStamp, NewPrescriptionDto newPrescription)
            :base(aggregateId, eventType, timeStamp)
        {
            NewPrescription = newPrescription;
        }
    }
}
