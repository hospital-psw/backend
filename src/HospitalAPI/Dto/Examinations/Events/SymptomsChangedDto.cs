namespace HospitalAPI.Dto.Examinations.Events
{
    using HospitalLibrary.Core.Model.Enums;
    using System;

    public class SymptomsChangedDto : ExaminationEventDto
    {
        public int SymptomId { get; set; }

        public SymptomEventStatus Status { get; set; }

        public SymptomsChangedDto()
        {

        }

        public SymptomsChangedDto(int aggregateId, ExaminationEventType eventType, DateTime timeStamp, int symptomId, SymptomEventStatus status)
            : base(aggregateId, eventType, timeStamp)
        {
            SymptomId = symptomId;
            Status = status;
        }
    }
}
