namespace HospitalAPI.Mappers.Examinations.Events
{
    using HospitalAPI.Dto.Examinations.Events;
    using HospitalLibrary.Core.Model.Events;

    public static class ExaminationStartedMapper
    {
        public static ExaminationStarted DtoToEntity(ExaminationStartedDto dto)
        {
            return new ExaminationStarted(dto.AggregateId, dto.TimeStamp, dto.EventType.ToString(), dto.AppointmentId, dto.UserId);
        }
    }
}
