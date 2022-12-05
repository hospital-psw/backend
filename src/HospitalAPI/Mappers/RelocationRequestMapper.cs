namespace HospitalAPI.Mappers
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Service.Core;
    using System;

    public class RelocationRequestMapper
    {

        public static RelocationRequest EntityDtoToEntity(RelocationRequestDto dto, Room fromRoom, Room toRoom, Equipment equipment)
        {
            RelocationRequest relocationRequest = RelocationRequest.Create(fromRoom, toRoom, equipment, dto.Quantity, dto.StartTime, dto.Duration);
            return relocationRequest;
        }
    }
}
