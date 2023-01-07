namespace HospitalAPI.Mappers.Examinations.Events
{
    using HospitalAPI.Dto.Examinations.Events;
    using HospitalLibrary.Core.Model.Events;

    public class DescriptionCreatedMapper
    {
        public static DescriptionCreated DtoToEntity(DescriptionCreatedDto dto)
        {
            return new DescriptionCreated(dto.AggregateId, dto.TimeStamp, dto.EventType.ToString(), dto.Description);
        }
    }
}
