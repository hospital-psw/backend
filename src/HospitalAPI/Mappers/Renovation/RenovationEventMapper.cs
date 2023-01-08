namespace HospitalAPI.Mappers.Renovation
{
    using HospitalAPI.Dto;
    using HospitalAPI.Dto.Enum;
    using HospitalLibrary.Core.Infrastucture;
    using HospitalLibrary.Core.Model;
    using System;

    public class RenovationEventMapper
    {
        public static RenovationEvent EntityDtoToEntity(RenovationEventDto dto)
        {
            return new RenovationEvent(dto.AggregateId, dto.TimeStamp, dto.EventType.ToString(), dto.Type);
        }
    }
}
