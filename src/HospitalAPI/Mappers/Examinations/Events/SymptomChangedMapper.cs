namespace HospitalAPI.Mappers.Examinations.Events
{
    using HospitalAPI.Dto.Examinations.Events;
    using HospitalLibrary.Core.Model.Events;
    using HospitalLibrary.Core.Model.Examinations;

    public static class SymptomChangedMapper
    {
        public static SymptomsChanged DtoToEntity(SymptomsChangedDto dto)
        {
            return new SymptomsChanged(dto.AggregateId, dto.TimeStamp, dto.EventType.ToString(), dto.SymptomId, dto.Status, dto.UserId);
        }
    }
}
