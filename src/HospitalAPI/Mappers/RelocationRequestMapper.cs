namespace HospitalAPI.Mappers
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.Model;

    public class RelocationRequestMapper
    {
        public static RelocationRequest EntityDtoToEntity(RelocationRequestDto dto)
        {
            RelocationRequest relocationRequest = new RelocationRequest();

            relocationRequest.FromRoom = RoomMapper.EntityDtoToEntity(dto.FromRoom);
            relocationRequest.ToRoom = RoomMapper.EntityDtoToEntity(dto.ToRoom);
            relocationRequest.Equipment = dto.Equipment;
            relocationRequest.Quantity = dto.Quantity;
            relocationRequest.StartTime = dto.StartTime;
            relocationRequest.Duration = dto.Duration;
            
            return relocationRequest;
        }
    }
}
