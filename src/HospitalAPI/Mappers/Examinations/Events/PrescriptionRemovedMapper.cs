namespace HospitalAPI.Mappers.Examinations.Events
{
    using HospitalAPI.Dto.Examinations.Events;
    using HospitalLibrary.Core.Model.Events;

    public class PrescriptionRemovedMapper
    {
        public static PrescriptionRemoved DtoToEntity(PrescriptionRemovedDto dto)
        {
            return new PrescriptionRemoved(dto.AggregateId, dto.TimeStamp, dto.EventType.ToString(), dto.PrescriptionId);
        }
    }
}
