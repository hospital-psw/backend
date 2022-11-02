namespace HospitalAPI.Mappers
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.Model;

    public class RoomMapper
    {
        public static RoomDto EntityToEntityDto(Room room)
        {
            RoomDto dto = new RoomDto();

            dto.Id = room.Id;
            dto.Number = room.Number;
            dto.Purpose = room.Purpose;
            dto.WorkingHours = WorkingHoursMapper.EntityToEntityDto(room.WorkingHours);
            dto.Floor = FloorMapper.EntityToEntityDto(room.Floor);

            return dto;
        }

        public static Room EntityDtoToEntity(RoomDto dto)
        {
            Room room = new Room();

            room.Id = dto.Id;
            room.Floor = FloorMapper.EntityDtoToEntity(dto.Floor);
            room.Number = dto.Number;
            room.Purpose = dto.Purpose;
            room.WorkingHours = WorkingHoursMapper.EntityDtoToEntity(dto.WorkingHours);

            return room;
        }
    }
}
