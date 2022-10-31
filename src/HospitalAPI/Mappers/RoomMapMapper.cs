namespace HospitalAPI.Mappers
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.Model;

    public class RoomMapMapper
    {
        public static RoomMapDto EntityToEntityDto(RoomMap roomMap)
        {
            RoomMapDto dto = new RoomMapDto();

            dto.Room = RoomMapper.EntityToEntityDto(roomMap.Room);
            dto.X = roomMap.X;
            dto.Z = roomMap.Z;

            return dto;
        }
    }
}
