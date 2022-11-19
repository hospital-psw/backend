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
            RelocationRequest relocationRequest = new RelocationRequest();

            relocationRequest.FromRoom = fromRoom;
            relocationRequest.ToRoom = toRoom;
            relocationRequest.Equipment = equipment;
            relocationRequest.Quantity = dto.Quantity;
            relocationRequest.StartTime = dto.StartTime;
            relocationRequest.Duration = dto.Duration;

            return relocationRequest;
        }
    }
}
