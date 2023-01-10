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

        public SymptomsChangedDto(int aggregateId, ExaminationEventType eventType, DateTime timeStamp, int symptomId, SymptomEventStatus status, int userId)
            : base(aggregateId, eventType, timeStamp, userId)
        {
            SymptomId = symptomId;
            Status = status;
        }
    }
}
