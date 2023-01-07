namespace HospitalAPI.Mappers.Examinations.Events
{
    using HospitalAPI.Dto.Examinations.Events;
    using HospitalLibrary.Core.Model.Events;

    public class ExaminationFinishedMapper
    {
        public static ExaminationFinished DtoToEntity(ExaminationFinishedDto dto)
        {
            return new ExaminationFinished(dto.AggregateId, dto.TimeStamp, dto.EventType.ToString(), dto.AppointmentId);
        }
    }
}
