namespace HospitalAPI.Mappers.Examinations.Events
{
    using HospitalAPI.Dto.Examinations.Events;
    using HospitalLibrary.Core.Model.Events;

    public class ExaminationEventMapper
    {
        public static ExaminationEvent DtoToEntity(ExaminationEventDto dto)
        {
            return new ExaminationEvent(dto.AggregateId, dto.TimeStamp, dto.EventType.ToString());
        }
    }
}
